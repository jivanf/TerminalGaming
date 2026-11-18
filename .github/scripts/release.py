"""Validate release metadata and package only the published mod files."""

import argparse
import json
import re
import xml.etree.ElementTree as ET
from pathlib import Path
from zipfile import ZIP_DEFLATED, ZipFile, ZipInfo


def version():
    manifest = json.loads(Path("assets/manifest.json").read_text(encoding="utf-8-sig"))
    package_version = manifest["version_number"]
    release_version = json.loads(Path(".release-please-manifest.json").read_text(encoding="utf-8"))["."]
    assembly_version = ET.parse("TerminalGaming/TerminalGaming.csproj").findtext("PropertyGroup/Version")
    if not re.fullmatch(r"\d+\.\d+\.\d+", package_version) or len(
        {package_version, release_version, assembly_version}
    ) != 1:
        raise SystemExit("Package, release manifest, and assembly versions must agree.")
    return package_version


def notes(changelog, release_version):
    headings = list(re.finditer(r"(?m)^#{1,2} (?:\[)?\d+\.\d+\.\d+[^\n]*$", changelog))
    if not headings or not re.match(
        rf"^#{{1,2}} \[?{re.escape(release_version)}(?=\]|\s|$)", headings[0].group()
    ):
        raise SystemExit(f"Missing top changelog entry for {release_version}.")
    end = headings[1].start() if len(headings) > 1 else len(changelog)
    return changelog[headings[0].end():end].strip(), headings[0].end(), end


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("mode", choices=("version", "notes", "package", "normalize", "verify"))
    parser.add_argument("--changelog", type=Path, default=Path("CHANGELOG.md"))
    parser.add_argument("--dll", type=Path)
    parser.add_argument("--output", type=Path)
    args = parser.parse_args()
    release_version = version()
    if args.mode == "version":
        print(release_version)
        return

    changelog = args.changelog.read_text(encoding="utf-8-sig")
    current, start, end = notes(changelog, release_version)
    if args.mode == "normalize":
        current = re.sub(r"(?m)^([-*] )\*\*[^*\r\n]+:\*\* +", r"\1", current)
        suffix = "\n\n" if end < len(changelog) else "\n"
        args.changelog.write_text(changelog[:start] + "\n\n" + current + suffix + changelog[end:], encoding="utf-8", newline="\n")
    elif args.mode == "notes":
        if not current:
            raise SystemExit("Release notes must not be empty.")
        headings = re.findall(r"(?m)^#{3,}\s+(.+)$", current)
        if not headings or set(headings) - {"Features", "Changes", "Fixes"}:
            raise SystemExit("Release notes may only use Features, Changes, and Fixes headings.")
        if re.search(r"(?m)^[-*] \*\*[^*\r\n]+:\*\* +", current):
            raise SystemExit("Remove commit scopes from the release notes.")
        print(current)
    elif args.mode == "verify":
        if args.output is None:
            parser.error("verify requires --output")
        with ZipFile(args.output) as archive:
            expected = ["CHANGELOG.md", "README.md", "TerminalGaming.dll", "icon.png", "manifest.json"]
            if sorted(archive.namelist()) != expected or archive.testzip() is not None:
                raise SystemExit("The ZIP contains unexpected or damaged files.")
            for name, path in (
                ("manifest.json", Path("assets/manifest.json")),
                ("icon.png", Path("assets/icon.png")),
                ("README.md", Path("README.md")),
                ("CHANGELOG.md", args.changelog),
            ):
                if archive.read(name) != path.read_bytes():
                    raise SystemExit(f"{name} does not match the approved release commit.")
            if not archive.read("TerminalGaming.dll").startswith(b"MZ"):
                raise SystemExit("TerminalGaming.dll is not a Windows assembly.")
    else:
        if args.dll is None or args.output is None:
            parser.error("package requires --dll and --output")
        files = {
            "TerminalGaming.dll": args.dll,
            "manifest.json": Path("assets/manifest.json"),
            "icon.png": Path("assets/icon.png"),
            "README.md": Path("README.md"),
            "CHANGELOG.md": args.changelog,
        }
        args.output.parent.mkdir(parents=True, exist_ok=True)
        with ZipFile(args.output, "w") as archive:
            for name, source in sorted(files.items()):
                archive.writestr(ZipInfo(name), source.read_bytes(), compress_type=ZIP_DEFLATED)


if __name__ == "__main__":
    main()

using System.Collections;
using BepInEx.Configuration;
using TerminalGaming.Extensions;
using TerminalGaming.Games.Pong.Elements;
using TerminalGaming.Games.Pong.EventArgs;
using TerminalGaming.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TerminalGaming.Games.Pong;

internal enum PaddleDirection
{
    Up,
    Down,
}

public sealed class PongGameScript : GameScript
{
    private const float InitialPaddleSpeed = 320;
    private const float InitialBallSpeed = InitialPaddleSpeed + 20;

    private const float MaxPaddleSpeed = 650;

    private const int SpeedIncrement = 5;

    private float paddleSpeed = InitialPaddleSpeed;
    private float ballSpeed = InitialBallSpeed;

    private PaddleElement leftPaddle = null!;
    private RectTransform leftPaddleTransform = null!;

    private PaddleElement rightPaddle = null!;
    private RectTransform rightPaddleTransform = null!;

    private BallElement ball = null!;
    private RectTransform ballTransform = null!;

    private ScoreElement leftScore = null!;
    private ScoreElement rightScore = null!;

    /// <summary>
    /// A normalized vector representing the ball's current direction. Since the ball is moving all the time,
    /// this property will always have a value.
    /// </summary>
    private Vector2 ballDirection;

    /// <summary>
    /// <para>
    ///     The current destination of the ball. Since the ball is moving all the time, this property will always
    ///     have a value.
    /// </para>
    ///
    /// <para>
    ///     The ball's destination is defined manually because:
    ///     <list type="number">
    ///         <item>
    ///             The container doesn't have any collisions, which means the ball would go off the screen if
    ///             we were to move it by increasing its position every frame.
    ///         </item>
    ///         <item>
    ///             Increasing the ball's position every frame would also require a check to see if the ball has reached one
    ///             of container's boundaries, which would be inefficient.
    ///         </item>
    ///     </list>
    /// </para>
    /// </summary>
    private Vector2 ballDestination;

    protected override void Start()
    {
        base.Start();

        // Paddles
        this.leftPaddle = UIManager.Instance.GetRenderable<PaddleElement>("LeftPaddleElement");
        this.leftPaddleTransform = this.leftPaddle.RectTransform;

        this.rightPaddle = UIManager.Instance.GetRenderable<PaddleElement>("RightPaddleElement");
        this.rightPaddleTransform = this.rightPaddle.RectTransform;

        // Ball
        this.ball = UIManager.Instance.GetRenderable<BallElement>();
        this.ballTransform = this.ball.RectTransform;

        // Scores
        this.leftScore = UIManager.Instance.GetRenderable<ScoreElement>("LeftScoreElement");
        this.rightScore = UIManager.Instance.GetRenderable<ScoreElement>("RightScoreElement");

        // Subscribe to events
        PongGameEvents.RallyStart += this.PongGame_RallyStart;
        PongGameEvents.BallHit += this.PongGame_BallHit;

        // Let's roll
        PongGameEvents.OnRallyStart(new RallyStartEventArgs { WinnerScore = null });
    }

    private void Update()
    {
        if (
            new KeyboardShortcut(KeyCode.W).IsPressed()
            || new KeyboardShortcut(KeyCode.S).IsPressed()
        )
        {
            PaddleDirection direction = new KeyboardShortcut(KeyCode.W).IsPressed()
                ? PaddleDirection.Up
                : PaddleDirection.Down;
            this.leftPaddleTransform.anchoredPosition = this.MovePaddle(
                this.leftPaddleTransform,
                direction
            );
        }

        if (
            new KeyboardShortcut(KeyCode.UpArrow).IsPressed()
            || new KeyboardShortcut(KeyCode.DownArrow).IsPressed()
        )
        {
            PaddleDirection direction = new KeyboardShortcut(KeyCode.UpArrow).IsPressed()
                ? PaddleDirection.Up
                : PaddleDirection.Down;
            this.rightPaddleTransform.anchoredPosition = this.MovePaddle(
                this.rightPaddleTransform,
                direction
            );
        }

        PaddleElement? collidedPaddle = this.ballTransform.CollidesWith(this.leftPaddleTransform)
            ? this.leftPaddle
            : this.ballTransform.CollidesWith(this.rightPaddleTransform)
                ? this.rightPaddle
                : null;

        if (collidedPaddle is not null)
        {
            if (!collidedPaddle.IsHittingBall)
            {
                collidedPaddle.IsHittingBall = true;

                PongGameEvents.OnBallHit(new BallHitEventArgs { HittingPaddle = collidedPaddle });

                float xDirection = Equals(collidedPaddle, this.leftPaddle) ? 1 : -1;

                // Get the ball's position as a normalized value relative to the paddle's top and bottom borders
                float directionInterpolant = Mathf.InverseLerp(
                    collidedPaddle.RectTransform.GetLocalBorderPosition(BorderPosition.Bottom).y,
                    collidedPaddle.RectTransform.GetLocalBorderPosition(BorderPosition.Top).y,
                    this.ballTransform.localPosition.y
                );

                this.ballDirection = Vector2.Lerp(
                    new Vector2(xDirection, -1).normalized,
                    new Vector2(xDirection, 1).normalized,
                    directionInterpolant
                );

                this.SetBallDestination();
            }
        }
        else
        {
            this.leftPaddle.IsHittingBall = false;
            this.rightPaddle.IsHittingBall = false;
        }

        // BallElement stopped moving because it reached the container's boundary
        if (
            this.ballDestination != Vector2.zero
            && Vector2.Distance(this.ballTransform.anchoredPosition, this.ballDestination) < 0.0001
        )
        {
            Vector2? ballNormal = null;

            float containerMiddleWidth =
                this.ContainerRectTransform.GetWidth()
                / 2
                * Mathf.Sign(this.ballTransform.anchoredPosition.x);

            float containerMiddleHeight =
                this.ContainerRectTransform.GetHeight()
                / 2
                * Mathf.Sign(this.ballTransform.anchoredPosition.y);

            if (Mathf.Approximately(this.ballTransform.anchoredPosition.x, containerMiddleWidth))
            {
                PongGameEvents.OnRallyStart(
                    new RallyStartEventArgs
                    {
                        WinnerScore = containerMiddleWidth > 0 ? this.leftScore : this.rightScore
                    }
                );

                return;
            }

            if (Mathf.Approximately(this.ballTransform.anchoredPosition.y, containerMiddleHeight))
            {
                ballNormal = containerMiddleHeight > 0 ? Vector2.down : Vector2.up;
            }

            this.ballDirection = Vector2.Reflect(this.ballDirection, ballNormal!.Value);
            this.SetBallDestination();
        }

        this.MoveBall();
    }

    private void OnDestroy()
    {
        PongGameEvents.RallyStart -= this.PongGame_RallyStart;
        PongGameEvents.BallHit -= this.PongGame_BallHit;
    }

    private void PongGame_RallyStart(RallyStartEventArgs args)
    {
        args.WinnerScore?.Increment();

        this.StartCoroutine(this.ResetRound());
    }

    private void PongGame_BallHit(BallHitEventArgs args)
    {
        if (this.paddleSpeed >= MaxPaddleSpeed)
        {
            return;
        }

        this.paddleSpeed += SpeedIncrement;
        this.ballSpeed += SpeedIncrement;
    }

    private IEnumerator ResetRound()
    {
        this.ballTransform.anchoredPosition = Vector2.zero;

        this.ballDirection = Vector2.zero;
        this.ballDestination = Vector2.zero;

        this.paddleSpeed = InitialPaddleSpeed;
        this.ballSpeed = InitialBallSpeed;

        yield return new WaitForSeconds(1);

        this.SetInitialBallDirection();
        this.SetBallDestination();
    }

    private Vector2 MovePaddle(RectTransform paddle, PaddleDirection direction)
    {
        // Maintain frame-rate independence with `Time.deltaTime`.
        float distance = this.paddleSpeed * Time.deltaTime;

        float targetY =
            direction == PaddleDirection.Up
                ? this.ContainerRectTransform.GetHeight() / 2 - paddle.GetHeight() / 2
                : -this.ContainerRectTransform.GetHeight() / 2 + paddle.GetHeight() / 2;

        return Vector2.MoveTowards(
            paddle.anchoredPosition,
            new Vector2(this.leftPaddleTransform.GetWidth() / 2, targetY),
            distance
        );
    }

    private void MoveBall()
    {
        Vector2 ballPosition = this.ballTransform.anchoredPosition;

        this.ballTransform.anchoredPosition = Vector2.MoveTowards(
            ballPosition,
            this.ballDestination,
            this.ballSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Set a random direction as the ball's initial direction.
    /// </summary>
    private void SetInitialBallDirection()
    {
        this.ballDirection = new Vector2(Random.Range(-1, 1) * 2 + 1, Random.Range(-0.50f, 0.50f));
    }

    private void SetBallDestination()
    {
        /*
         * This method needs to determine, along with the ball's direction, if the ball's destination should be on the container's
         * x-boundary (left or right border, based on whether the ball is going left or right) or
         * y-boundary (top or bottom borders, based on whether the ball is going up or down).
         *
         * We know two things about the ball's destination at this point:
         * 1. It needs to be one of the container's boundaries.
         * 2. If the ball is going right and/or up, the x-boundary and/or y-boundary will be positive, and vice versa.
         *
         * With this, we have one component of the destination's vector. To find the other component, we'll use the
         * line formula, which allows us to find the destination's x or y value using the starting position's
         * x *and* y values (the current position of the ball) and one of the destination's x or y values,
         * which would be the destination's vector component we have (the x- or y-boundary). The line formula is:
         *
         * y = mx + b
         *
         * However, we still don't know if we'll use the x- or y-boundary for the destination's vector and whether
         * we'll use the x- or y-value derived from the formula, which we'll call "x-target" and "y-target"
         * respectively. To determine this, we'll use both boundaries to calculate 2 different destination positions:
         * 1. X: x-boundary, Y: y-target
         * 2. X: x-target, Y: y-boundary
         *
         * One of these positions is wrong because it's out of bounds. For example, if the ball were to bounce off the
         * right paddle down and to the left (45-degree angle), the first position (x-boundary and y-target) would be
         * wrong because in order to reach the x-boundary (left boundary), it would have to go out of bounds to
         * reach y-target. On the other hand, the second position can be reached because both the y-boundary
         * (bottom boundary) and the x-target are in bounds. Therefore, this position would be the ball's destination.
         *
         */

        Vector2 ballPosition = this.ballTransform.anchoredPosition;

        // Divide by two because we're working with the ball's position relative to the container
        float xBoundary = this.ContainerRectTransform.GetWidth() / 2;
        float yBoundary = this.ContainerRectTransform.GetHeight() / 2;

        // If the ball is moving right or up, the boundary the ball will move towards will be positive
        // because the direction is positive, and vice versa
        xBoundary = this.ballDirection.x > 0 ? xBoundary : -xBoundary;
        yBoundary = this.ballDirection.y > 0 ? yBoundary : -yBoundary;

        // m = dy / dx
        float slope = this.ballDirection.y / this.ballDirection.x;

        float yIntercept = ballPosition.y - slope * ballPosition.x;

        float yTarget = slope * xBoundary + yIntercept;

        // Rearrange the formula to get the x-target
        float xTarget = (yBoundary - yIntercept) / slope;

        // Check which target (x-target or y-target) is in bounds
        float target =
            (this.ballDirection.x > 0 ? xBoundary >= xTarget : xBoundary <= xTarget)
                ? xTarget
                : yTarget;

        this.ballDestination = new Vector2(
            Equals(target, xTarget) ? xTarget : xBoundary,
            Equals(target, yTarget) ? yTarget : yBoundary
        );
    }
}

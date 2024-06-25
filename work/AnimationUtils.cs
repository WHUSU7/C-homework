using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace work
{
	public class AnimationUtils
	{
		private static bool isAnimating = false;
		public static void ChessDropDownAnimation(Button btn, int x, double maxHeight)
		{
			//创建动画
			DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames();
			dakY.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
			//添加关键帧

			SplineDoubleKeyFrame startKf = new SplineDoubleKeyFrame();
			//关键帧起始百分比（0-1）
			startKf.KeyTime = KeyTime.FromPercent(0);
			double startHeight = x * maxHeight * 0.142857;
			//终点相对于元素本身的位置
			startKf.Value = -startHeight;


			SplineDoubleKeyFrame endKf = new SplineDoubleKeyFrame();
			endKf.KeyTime = KeyTime.FromPercent(1);
			endKf.Value = 0;
			KeySpline ks = new KeySpline();
			ks.ControlPoint1 = new Point(0.42, 0);
			ks.ControlPoint2 = new Point(0.58, 1);
			endKf.KeySpline = ks;


			dakY.KeyFrames.Add(startKf);
			dakY.KeyFrames.Add(endKf);
			btn.RenderTransform = new TranslateTransform();
			btn.RenderTransform.BeginAnimation(TranslateTransform.YProperty, dakY);


		}








		public static void ChessRotateAnimation(Button btn)
		{

			DoubleAnimationUsingKeyFrames rotateAnimation = new DoubleAnimationUsingKeyFrames();
			rotateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(2000));

			// 动画开始时
			DiscreteDoubleKeyFrame startFrame = new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));

			// 10秒后旋转600度（每秒60度）
			LinearDoubleKeyFrame endFrame = new LinearDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)));

			rotateAnimation.KeyFrames.Add(startFrame);
			rotateAnimation.KeyFrames.Add(endFrame);
			rotateAnimation.RepeatBehavior = RepeatBehavior.Forever;

			btn.RenderTransform = new RotateTransform();
			btn.RenderTransformOrigin = new Point(0.5, 0.5);
			btn.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
		}


        private static void ShowParticleEffect(Button btn, double x, double y, Canvas canvas)
        {
            // 确保找到了 Canvas
            if (canvas != null)
            {
                // 循环创建粒子并设置动画
                double r = btn.ActualWidth / 2;
                Random rand = new Random();
                for (int i = 0; i < 50; i++)
                {
                    // 创建新的粒子
                    var particle = new Ellipse
                    {
                        Width = 1,
                        Height = 1,
                        Fill = new SolidColorBrush(Color.FromArgb(
                            255,
                            (byte)rand.Next(256), // 随机R值
                            (byte)rand.Next(256), // 随机G值
                            (byte)rand.Next(256)  // 随机B值
                        )),
                        Opacity = 0.7
                    };

                    // 将粒子添加到 Canvas 上
                    canvas.Children.Add(particle);

                    // 设置粒子初始位置为按钮位置
                    Canvas.SetLeft(particle, x);
                    Canvas.SetTop(particle, y);

                    // 随机速度和方向
                    double speed = rand.Next(10, 15); // 随机速度
                    double angle = rand.Next(0, 360) * Math.PI / 180; // 随机角度
                    double dx = speed * Math.Cos(angle);
                    double dy = speed * Math.Sin(angle);

                    // 创建并设置粒子动画
                    DoubleAnimation animX = new DoubleAnimation(x + 5 * dx, x + 6 * dx, TimeSpan.FromSeconds(1));
                    DoubleAnimation animY = new DoubleAnimation(y + 5 * dy, y + 6 * dy, TimeSpan.FromSeconds(1));
                    animX.Completed += (s, e) => canvas.Children.Remove(particle); // 动画完成时移除粒子
                    animY.Completed += (s, e) => canvas.Children.Remove(particle); // 动画完成时移除粒子

                    particle.BeginAnimation(Canvas.LeftProperty, animX);
                    particle.BeginAnimation(Canvas.TopProperty, animY);

                    // 渐隐动画
                    DoubleAnimation opacityAnim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                    particle.BeginAnimation(Ellipse.OpacityProperty, opacityAnim);

                    // 大小变化动画
                    DoubleAnimation sizeAnim = new DoubleAnimation(5, 1, TimeSpan.FromSeconds(1));
                    particle.BeginAnimation(Ellipse.WidthProperty, sizeAnim);
                    particle.BeginAnimation(Ellipse.HeightProperty, sizeAnim);
                }
            }
        }







        //两个方法一起调用
        public static async Task allAnimation(Button btn, int x, double maxHeight, Canvas canvas)
        {
            
            int column = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);
            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;
            double buttonWidthSize = canvasWidth * 0.142857;
            double buttonHeightSize = canvasHeight * 0.166667;
            double mx = (column + 0.5) * buttonWidthSize;
            double my = (row + 0.5) * buttonHeightSize;
            double ax = column * buttonWidthSize;
            double ay = row * buttonHeightSize;

            // 创建和配置 RotateTransform 和 TranslateTransform
            RotateTransform rotateTransform = new RotateTransform();
            TranslateTransform translateTransform = new TranslateTransform();
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(translateTransform);
            btn.RenderTransform = transformGroup;

            // 旋转部分
            DoubleAnimationUsingKeyFrames rotateAnimation = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(2000))
            };
            DiscreteDoubleKeyFrame startFrame = new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            LinearDoubleKeyFrame endFrame = new LinearDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)));
            rotateAnimation.KeyFrames.Add(startFrame);
            rotateAnimation.KeyFrames.Add(endFrame);
            rotateAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btn.RenderTransformOrigin = new Point(0.5, 0.5);

            // 平移部分
            DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(600))
            };
            SplineDoubleKeyFrame startKf = new SplineDoubleKeyFrame
            {
                KeyTime = KeyTime.FromPercent(0),
                Value = -x * maxHeight * 0.142857
            };
            SplineDoubleKeyFrame endKf = new SplineDoubleKeyFrame
            {
                KeyTime = KeyTime.FromPercent(1),
                Value = 0,
                KeySpline = new KeySpline(0.7, 0, 1, 1)
            };
            dakY.KeyFrames.Add(startKf);
            dakY.KeyFrames.Add(endKf);

            // 拖尾效果的圆形列表
            List<Ellipse> trailCircles = new List<Ellipse>();
            int maxTrailCount = 20; // 最大拖尾数量

            // 定时器，用于创建和更新拖尾
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(5)
            };
            timer.Tick += (s, e) =>
            {
                double currentY = translateTransform.Y;
                double trailX = ax;
                double trailY = ay + currentY - btn.ActualHeight / 2;
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(@"..\..\Images\falling1.gif", UriKind.RelativeOrAbsolute))
                };

                // 创建新的圆形拖尾并设置位置
                Ellipse trailCircle = new Ellipse
                {
                    Width = btn.ActualWidth,
                    Height = btn.ActualHeight,
                    Fill = imageBrush
                };
                canvas.Children.Add(trailCircle);
                Canvas.SetLeft(trailCircle, trailX);
                Canvas.SetTop(trailCircle, trailY);

                // 将新创建的圆形拖尾添加到列表中
                trailCircles.Add(trailCircle);

                // 根据需要，限制圆形拖尾的数量，以免过多影响性能
                if (trailCircles.Count > maxTrailCount)
                {
                    canvas.Children.Remove(trailCircles[0]); // 移除列表中的第一个圆形拖尾
                    trailCircles.RemoveAt(0); // 从列表中移除第一个圆形拖尾
                }
            };

            // 创建 TaskCompletionSource 用于等待动画完成
            var tcs = new TaskCompletionSource<bool>();

            // 在动画完成时处理拖尾的淡出效果并设置 TaskCompletionSource 结果
            dakY.Completed += (s, e) =>
            {
                timer.Stop(); // 停止定时器
                ShowParticleEffect(btn, mx, my, canvas);
                foreach (Ellipse trailCircle in trailCircles)
                {
                    // 创建圆形拖尾的淡出动画
                    DoubleAnimation trailFadeOut = new DoubleAnimation
                    {
                        Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                        From = 1,
                        To = 0
                    };
                    trailCircle.BeginAnimation(Ellipse.OpacityProperty, trailFadeOut);
                    // 在淡出动画完成后，从画布中移除圆形拖尾
                    trailFadeOut.Completed += (s2, a) => canvas.Children.Remove(trailCircle);
                }

                // 清空圆形拖尾列表
                trailCircles.Clear();

                // isAnimating = false; // 动画完成，重置动画状态
                tcs.SetResult(true); // 设置 TaskCompletionSource 结果
            };



            // 开始动画和定时器
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, dakY);
            timer.Start();

            await tcs.Task; // 等待动画完成
        }


       
    }

}


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

        public static void ShowParticleEffect(Button btn, double x, double y, Canvas canvas)
        {
            // 确保找到了 Canvas
            if (canvas != null)
            {
                //Point btnPosition = btn.TransformToAncestor(canvas).Transform(new Point(0, 0));
                //double btnCenterX = btnPosition.X - btn.ActualWidth / 2 ;
                //double btnCenterY = btnPosition.Y - btn.ActualHeight / 2;
                //MessageBox.Show(btnPosition.X.ToString());
                //MessageBox.Show(btnPosition.Y.ToString());
                // 循环创建粒子并设置动画
                double r = btn.ActualWidth / 2;
                Random rand = new Random();
                for (int i = 0; i < 500; i++)
                {
                    // 创建新的粒子
                    var particle = new Ellipse();
                    particle.Width = particle.Height = 5;
                    particle.Fill = Brushes.White;

					// 将粒子添加到 Canvas 上
					canvas.Children.Add(particle);

					// 设置粒子初始位置为按钮位置

					Canvas.SetLeft(particle, x);
					Canvas.SetTop(particle, y);

					// 随机速度和方向
					double speed = 10;
					double angle = rand.Next(0, 360) * Math.PI / 180; // 不同的角度
					double dx = r * Math.Cos(angle);
					double dy = r * Math.Sin(angle);

                    // 创建并设置粒子动画
                    DoubleAnimation animX = new DoubleAnimation(x, x + 3 * dx, TimeSpan.FromSeconds(0.3));
                    DoubleAnimation animY = new DoubleAnimation(y, y + 3 * dy, TimeSpan.FromSeconds(0.3));
                    animX.Completed += (s, e) => canvas.Children.Remove(particle); // 动画完成时移除粒子
                    animY.Completed += (s, e) => canvas.Children.Remove(particle); // 动画完成时移除粒子

					particle.BeginAnimation(Canvas.LeftProperty, animX);
					particle.BeginAnimation(Canvas.TopProperty, animY);
					// 渐隐动画
					DoubleAnimation opacityAnim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.5)); // 将持续时间延长以确保逐渐消失
					particle.BeginAnimation(Ellipse.OpacityProperty, opacityAnim);
				}
			}
			else
			{
				// 处理找不到 Canvas 的情况
			}
		}





        //两个方法一起调用
        public static void allAnimation(Button btn, int x, double maxHeight, Canvas canvas)
        {
            // 创建和配置 RotateTransform 和 TranslateTransform
            RotateTransform rotateTransform = new RotateTransform();
            TranslateTransform translateTransform = new TranslateTransform();
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(translateTransform);
            btn.RenderTransform = transformGroup;

            // 旋转部分
            DoubleAnimationUsingKeyFrames rotateAnimation = new DoubleAnimationUsingKeyFrames();
            rotateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
            DiscreteDoubleKeyFrame startFrame = new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            LinearDoubleKeyFrame endFrame = new LinearDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)));
            rotateAnimation.KeyFrames.Add(startFrame);
            rotateAnimation.KeyFrames.Add(endFrame);
            rotateAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btn.RenderTransformOrigin = new Point(0.5, 0.5);

            // 平移部分
            DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames();
            dakY.Duration = new Duration(TimeSpan.FromMilliseconds(600));
            SplineDoubleKeyFrame startKf = new SplineDoubleKeyFrame();
            startKf.KeyTime = KeyTime.FromPercent(0);
            double startHeight = x * maxHeight * 0.142857;
            startKf.Value = -startHeight;
            SplineDoubleKeyFrame endKf = new SplineDoubleKeyFrame();
            endKf.KeyTime = KeyTime.FromPercent(1);
            endKf.Value = 0;
            KeySpline ks = new KeySpline();
            ks.ControlPoint1 = new Point(0.7, 0);
            ks.ControlPoint2 = new Point(1, 1);
            endKf.KeySpline = ks;
            dakY.KeyFrames.Add(startKf);
            dakY.KeyFrames.Add(endKf);

            int column = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);
            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;
            double buttonWidthSize = canvasWidth * 0.142857;
            double buttonHeightSize = canvasHeight * 0.166667;
            double mx = (column + 0.5) * buttonWidthSize;
            double my = (row + 0.5) * buttonHeightSize;
            double ax =column * buttonWidthSize;
            double ay = row * buttonHeightSize;
            dakY.Completed += (sender, e) =>
            {
                ShowParticleEffect(btn, mx, my, canvas);
            };

            // 直接在 Transform 对象上开始动画
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, dakY);

            // 拖尾效果
            // 拖尾效果
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 1);
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));

            // 拖尾效果的圆形列表
            List<Ellipse> trailCircles = new List<Ellipse>();
            int maxTrailCount = 20; // 最大拖尾数量

            // 定时器，用于创建和更新拖尾
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += (sender, e) =>
            {
                double currentY = translateTransform.Y;
                double trailX = ax;
                double trailY = ay + currentY - btn.ActualHeight / 2;
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri(@"..\..\Images\falling1.gif", UriKind.RelativeOrAbsolute));
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

            // 开始定时器
            timer.Start();

            // 在动画完成时处理拖尾的淡出效果
            dakY.Completed += (sender, e) =>
            {
                timer.Stop(); // 停止定时器

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
                    trailFadeOut.Completed += (s, a) => canvas.Children.Remove(trailCircle);
                }

                // 清空圆形拖尾列表
                trailCircles.Clear();
            };
        }

}


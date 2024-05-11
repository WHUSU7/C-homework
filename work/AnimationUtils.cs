using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace work
{
    public class AnimationUtils
    {
        public static void  ChessDropDownAnimation(Button btn,int x,double maxHeight) { 
            //创建动画
            DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames();
            dakY.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            //添加关键帧

            SplineDoubleKeyFrame startKf = new SplineDoubleKeyFrame();
            //关键帧起始百分比（0-1）
            startKf.KeyTime = KeyTime.FromPercent(0);
            double startHeight = x*maxHeight* 0.142857;
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
            btn.RenderTransform.BeginAnimation(TranslateTransform.YProperty,dakY);

          
          
        }
       


        public static void ChessRotateAnimation(Button btn) {

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



    //两个方法一起调用
        public static void allAnimation(Button btn,int x, double maxHeight)
        {
            

            // 创建和配置 RotateTransform 和 TranslateTransform
            RotateTransform rotateTransform = new RotateTransform();
            TranslateTransform translateTransform = new TranslateTransform();
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(translateTransform);
            btn.RenderTransform = transformGroup;


            //rotate part
            DoubleAnimationUsingKeyFrames rotateAnimation = new DoubleAnimationUsingKeyFrames();
            rotateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
            DiscreteDoubleKeyFrame startFrame = new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            LinearDoubleKeyFrame endFrame = new LinearDoubleKeyFrame(360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)));
            rotateAnimation.KeyFrames.Add(startFrame);
            rotateAnimation.KeyFrames.Add(endFrame);
            rotateAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btn.RenderTransformOrigin = new Point(0.5,0.5);


            //transform part
            DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames();
            dakY.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            SplineDoubleKeyFrame startKf = new SplineDoubleKeyFrame();
            startKf.KeyTime = KeyTime.FromPercent(0);
            double startHeight = x * maxHeight * 0.142857;
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


            // 直接在Transform对象上开始动画
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, dakY);
        }

    }
}

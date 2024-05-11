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
        public static void  ChessDropDownAnimation(Button btn,int x,int y) { 
            //创建动画
        DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames();
            dakY.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            //添加关键帧

            SplineDoubleKeyFrame kf = new SplineDoubleKeyFrame();
            kf.KeyTime = KeyTime.FromPercent(1);
            kf.Value = 400;
            KeySpline ks = new KeySpline();
            ks.ControlPoint1 = new Point(0, 1);
            ks.ControlPoint2 = new Point(1, 0);
            kf.KeySpline = ks;
            dakY.KeyFrames.Add(kf);

            btn.RenderTransform.BeginAnimation(TranslateTransform.YProperty,dakY);

        
        }


    }
}

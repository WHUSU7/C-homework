   M S B A M L   `   `   `  ����?  ;work, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:work.Pages
work.Pages  5          work.Pages.HistoryPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:work.Pages     5        N� Title$  HistoryPage��5	       � 	Resources 5
       �� 
TargetType   commonBtnStyle �� Property �� Value �� 
TargetType �� CornerRadius �� ElementName  chessBoardImage �� 	UriSource  chessBoardBackground	 �� ImageSource  MoveRecordButtonStyle  ListBoxTemplate��%*  &	        &	 �    &	 �    7  &	      '�� Y    &	 :    �� 5   
   " ��6   .�5      ��  � y�$ Hidden=�6+   �� 5       �@J�IsHitTestVisible .  61   �� 5       � ��
 ���� 6,   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5       5      �� *ActualWidth+$ Button00��._�5      �� $��Center=�6/   $��Center=�5   *   5      5      5      5      �� 5   
   $2 ,pack://application:,,,/Images/chessBoard.png?�6.   �� 5   
   8	   62   �� 5#   
   " ��6.   .�5$      ��  � ��$ 10q�6(   �� 5%       � �� �	#FFADD8E66+   �� 5&       � ��
 �����6,   �� 5'       � ��$ 1q�60   �� 5(       � ��$ Stretch=�64   �� 5)       � ��$ Stretch=�62   �� 5*       � ��$ 14(�6)   �� 5+       � ��
 �   �6+   �� 5,       � ��$ 0q�6'   �� 5-       � �� 5.      �� 5/      " ��6&   .R�50      �� #��z��6!   #��z��51      #��z��52      $ 5��53      ._�54      �� $��Center=�6/   $��Center=�55   +   56      57      59      5:      �� 5<   
   " ��6   .�5=      ��  � ��$ 10q�6(   �� 5>       � ��$ 5q�6'   �� 5?       � ��$ Stretch=�6;   �� 5@       � ��$ Stretch=�69   �� 5A       � ��$ 60��6*   5B      �� 5D   
   " ��62   .R�5E      �� #��z��6   #��z��5F      #��z��5G      $ 10��5H      ._�5J      �� #��z��6   .��5K      �� 5L      5M      5N      5O      .��5P      � $	��800��6   $	��450��6   
 � RowDefinitions
 5Q   
   �� 5R      $��* �6   �� 5S      $��80 �6   .+�5U   
   � $��1��6    � ColumnDefinitions 5W      �� 5X      $��* �6#   �� 5Y      $��* �6#   �� 5Z      $��* �6#   .+�5]      �� -   5        $��border1��5]   %   $��0��6   $��50,5,0,5q�64   $ 5��6F   ��5^      �� 5_       �� 
StartPoint$	 0,1&�6*    �� EndPoint$	 1,1&�6;   .��5`      �  � Offset$ 0Q�6'    � Color$ #00CED1��62   � 5a      $ 1Q�6'   $ #59E6B5��62   5b      ._�5d      � .+�5e      �� }�. .+�5f      ��  �� Right$	 -40��6!   
 �� Top$ -9��64   $��4��6D   $	��110��6U   $	��110��6a   $	 100��6o   ���#ffffff J� Opacity$	 0.2Q�6�   $	��Top=�6�   $��Right=�6�    �� Left$ 93��6�   �� 5g      $	 -50��6!   $ 4��64   $��4��6C   $��30��6T   $��30��6_   $	 100��6k   ���#ffffff$	 0.2Q�6�   $��Bottom=�6�   $��Right=�6�   $	 180��6�   5i      �� 5j      -   5        
������ 
������ $��返回主页��5j   P     RoundedButtonStyle#��[ 6v   $��18(�6�   
�������6�   5k      5l      �� 5n      -   5        $��border2��5n   %   $��1��6   $��	25,5,25,5q�64   $ 5��6G   ��5o      �� 5p      $	 0,1&�6*   $	 1,1&�6;   .��5q      � $ 0Q�6'   $ #00CED1��62   � 5r      $ 1Q�6'   $ #59E6B5��62   5s      ._�5u      � .+�5v      �� }�. .+�5w      �� $	 -40��6!   $ 20��64   $��4��6D   $	��110��6U   $	��110��6a   $	 100��6o   ���#ffffff$	 0.2Q�6�   $	��Top=�6�   $��Right=�6�   $	 143��6�   �� 5x      $	 -50��6!   $ -3��64   $��4��6D   $��30��6U   $��30��6`   $	 100��6l   ���#ffffff$	 0.2Q�6�   $��Bottom=�6�   $��Right=�6�   $ 17��6�   5z      �� 5{      -   5        
������ 
������ $��	下一步��5{   P   #��[ 6q   $��18(�6�   
�������6�   5|      5}      �� 5~      -   5        $��border3��5~   %   $��2��6   $��0,5,50,5q�64   $ 5��6F   ��5      �� 5�      $	 0,1&�6*   $	 1,1&�6;   .��5�      � $ 0Q�6'   $ #00CED1��62   � 5�      $ 1Q�6'   $ #59E6B5��62   5�      ._�5�      � .+�5�      �� }�. .+�5�      �� $	 -40��6!   $	 -60��64   $��4��6E   $	��110��6V   $	��110��6b   $	 100��6p   ���#ffffff$	 0.2Q�6�   $	��Top=�6�   $��Right=�6�   $ 92��6�   �� 5�      $	 -50��6!   $ 29��64   $��4��6D   $��30��6U   $��30��6`   $	 100��6l   ���#ffffff$	 0.2Q�6�   $��Bottom=�6�   $��Right=�6�   $ 19��6�   5�      �� 5�      -   5        
������ 
������ $��	上一步��5�   P   #��[ 6q   $��18(�6�   
�������6�   5�      5�      5�      � 5�   
   $��0��6    5�      �� 5�      $	��150 �6#   �� 5�      $��* �6#   .+�5�      �� -   5         ��Name$ historyListBox��5�      $��5q�6.   
�������
�������$��0q�6�   #��[ 6�   ��69   �� *HistoryViewModel.MoveRecords+��5�      �� 5�      .R�5�      �� #��[ 6�   ��6!   �� *DataContext.SelectCommand+ �� RelativeSource ��  �� AncestorType$ ListBoxL���6�   �� .��5�      �  5�   "   �� 5�   &   $��2* �67   �� 5�   &   $��3* �67   .+�5�   "   �� $��0��6@   $��5q�6P   $��Center=�6[   ��6,   �� *id+6[   �� 5�   "   $��1��6D   $��5q�6T   $��Center=�6_   ��6,   �� *result+6_   5�      5�      5�      5�      � 5�      -   5         �Name$ myCanvasFatherGrid��5�      $��1��6/   $��1��6<   $��7��6L   $��6��6`   .+�5�      �� $ 30��6   
������ $��1q�6E   ���#eae6ce  DropShadowEffect J� Effect# [ 6n   ._�5�      �� -	   5         ��Name$ myCanvas��5�      #��[ 5�      ��5�      �� *ActualHeight+$ myCanvasFatherGrid����5�      �� *MainDataModel.CanvasWidth+ �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�.+�5�      � -
   5        $ ChessboardGrid��5�      ��67   �� *ActualWidth+$ myCanvas����6j   �� *ActualHeight+$ myCanvas��
 5�      �� 5�   "   62   �� 5�   "   62   �� 5�   "   62   �� 5�   "   62   �� 5�   "   62   �� 5�   "   62    5�      �� 5�   "   65   �� 5�   "   65   �� 5�   "   65   �� 5�   "   65   �� 5�   "   65   �� 5�   "   65   �� 5�   "   65   .+�5�      �� -   5         ��Name$ Button00��5�   B   $��0��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button01��5�   B   $��1��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button02��5�   B   $��2��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button03��5�   B   $��3��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button04��5�   B   $��4��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button05��5�   B   $��5��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button06��5�   B   $��6��6%   $��0��65   #��[  6T   �� 5�      -   5        $ Button10��5�   B   $��0��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button11��5�   B   $��1��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button12��5�   B   $��2��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button13��5�   B   $��3��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button14��5�   B   $��4��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button15��5�   B   $��5��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button16��5�   B   $��6��6%   $��1��65   #��[  6T   �� 5�      -   5        $ Button20��5�   B   $��0��6%   $��2��65   #��[  6T   �� 5�      -   5        $ Button21��5�   B   $��1��6%   $��2��65   #��[  6T   �� 5�      -   5        $ Button22��5�   B   $��2��6%   $��2��65   #��[  6T   �� 5�      -   5        $ Button23��5�   B   $��3��6%   $��2��65   #��[  6T   �� 5�      -   5        $ Button24��5�   B   $��4��6%   $��2��65   #��[  6T   �� 5�      -   5        $ Button25��5�   B   $��5��6%   $��2��65   #��[  6T   �� 5�      -   5        $ Button26��5�   B   $��6��6%   $��2��65   #��[  6T   �� 5�      -    5        $ Button30��5�   B   $��0��6%   $��3��65   #��[  6T   �� 5�      -!   5        $ Button31��5�   B   $��1��6%   $��3��65   #��[  6T   �� 5�      -"   5        $ Button32��5�   B   $��2��6%   $��3��65   #��[  6T   �� 5�      -#   5        $ Button33��5�   B   $��3��6%   $��3��65   #��[  6T   �� 5�      -$   5        $ Button34��5�   B   $��4��6%   $��3��65   #��[  6T   �� 5�      -%   5        $ Button35��5�   B   $��5��6%   $��3��65   #��[  6T   �� 5�      -&   5        $ Button36��5�   B   $��6��6%   $��3��65   #��[  6T   �� 5�      -'   5        $ Button40��5�   B   $��0��6%   $��4��65   #��[  6T   �� 5�      -(   5        $ Button41��5�   B   $��1��6%   $��4��65   #��[  6T   �� 5�      -)   5        $ Button42��5�   B   $��2��6%   $��4��65   #��[  6T   �� 5�      -*   5        $ Button43��5�   B   $��3��6%   $��4��65   #��[  6T   �� 5�      -+   5        $ Button44��5�   B   $��4��6%   $��4��65   #��[  6T   �� 5�      -,   5        $ Button45��5�   B   $��5��6%   $��4��65   #��[  6T   �� 5�      --   5        $ Button46��5�   B   $��6��6%   $��4��65   #��[  6T   �� 5�      -.   5        $ Button50��5�   B   $��0��6%   $��5��65   #��[  6T   �� 5�      -/   5        $ Button51��5�   B   $��1��6%   $��5��65   #��[  6T   �� 5�      -0   5        $ Button52��5�   B   $��2��6%   $��5��65   #��[  6T   �� 5�      -1   5        $ Button53��5�   B   $��3��6%   $��5��65   #��[  6T   �� 5�      -2   5        $ Button54��5�   B   $��4��6%   $��5��65   #��[  6T   �� 5�      -3   5        $ Button55��5�   B   $��5��6%   $��5��65   #��[  6T   �� 5�      -4   5        $ Button56��5�   B   $��6��6%   $��5��65   #��[  6T   5�      5�      5�      5�      5�      5�      5     
   M S B A M L   `   `   `  ����?  ;work, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:work.Utilwindowswork.Utilwindows  5   	       work.Utilwindows.showWin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:work.Utilwindows     5   	     � Title$  showWin��5   	   $	��450��6   $	��800��6&    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5         winningImage �� 	UriSource  winningImageBackground �� ImageSource �� 
TargetType �� Property �� Value��%5  &	        &	 H     7   '�� `     '�� �    �� 5   
   $4 .pack://application:,,,/Images/winningImage.png?�6+   �� 5   
   8   64   �� 5   
   " ��6   .�5      ��  � �� �#C7EDCC6+   �� 5       � ��$ 35(�6)   �� 5       � ��$ 10q�6'   �� 5       � ��$ 10q�6(   �� 5       � �� �#AAAAAA6,   �� 5       � ��$ 2q�60   �� 5       � ��$ Center=�64   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  � ��$ 25(�6)   �� 5       � ��$ 30q�6'   �� 5       � ��$	 400��6&   �� 5       � ��$ 10q�6(   �� 5       � �� �#AAAAAA6,   �� 5       � ��$ 2q�60   �� 5       � ��$ Center=�64   �� 5        � ��$ Center=�62   5!      5"      .��5#      � ��5$   
   �� 5%      	 �� 
StartPoint$		 0,0&�6"   
 �� EndPoint$	
 1,1&�63   .��5&      �  � Color$ #FFFAFA��6    � Offset$ 0Q�6/   � 5'      $ #E6E6FA��6   $ 1Q�6/   5(       � ColumnDefinitions 5*   
   �� 5+      $��* �6   �� 5,      $��2* �6   �� 5-      $��* �6    � RowDefinitions 5/   
   �� 50      $��* �6   �� 51      $
��Auto �6   �� 52      $
��Auto �6   �� 53      $��* �6   �� 54      $
��Auto �6   �� 55      $��* �6   .+�57   
   �� $��请输入新的用户名:��6   $��35(�6%   $��1��63   $��1��6@   $��Center=�6P   $��Center=�6m   $��10q�6�   �� 58   
   -   5        $��	nameInput��58      $��2��6#   $��1��60   $��10q�6@   �� 59   
   -   5         ��Name$ 
confirmBtn��59      $��确认��6%   $��4��62   $��1��6?   6O   5:      5;      
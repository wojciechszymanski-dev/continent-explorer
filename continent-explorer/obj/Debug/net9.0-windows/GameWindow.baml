   M S B A M L   `   `   `  ����M  Icontinent-explorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:continent_explorercontinent_explorer  5   	   ] YHelixToolkit.Core.Wpf, Version=2.26.0.0, Culture=neutral, PublicKeyToken=52aa3500039caf0dR=clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.Core.WpfHelixToolkit.Wpf 5   	   #    continent_explorer.GameWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:continent_explorer     5   	   VHelixToolkit=clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.Core.Wpf     5   	     � Title$  Continent Explorer��5	   	   $	��600��6$   $
��1000��61   ���#0A1128 � 	Resources 5         SpaceGradient �� 
StartPoint �� EndPoint � Color � Offset  	TitleText �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect	   ShadowDepth
   	Direction   Color   Opacity   
BlurRadius  ContinentButton �� 
TargetType �� CornerRadius J� Effect �� Triggers P� Property P� Value �� ScaleX �� ScaleY��%N  &	        &	 �     &	 �    �� 5   
   $	 0,0&�64   $	 1,1&�6E   .��5      � $ #0A1128��6   $ 0Q�6+   � 5      $ #1C3F60��6   $ 1Q�6+   5      �� 5   
   " ��6"   .�5      ��  � ��$ 
Montserrat)�6+   �� 5       � ��$
 Bold"�6+   �� 5       � �� �#E9F2F96+   �� 5       �@J�Effect 5        5      $	 2Q�6'   $	
 330Q�67   $ #4E8FC2��6G   $	 0.7Q�6W   $ 5Q�6e   5      5      �� 5   
   " ��6(   .�5      ��  � �� 5      �� 5       $	 0,0&�6*   $	 0,1&�6;   .��5!      � $ #2A4494��6'   $ 0Q�67   � 5"      $ #1C3F60��6'   $ 1Q�67   5#      5%      �� 5&       � �� �#E9F2F96+   �� 5'       � ��$ 
Montserrat)�6+   �� 5(       � ��$ SemiBold"�6+   �� 5)       � ��$ 14(�6)   �� 5*       � ��$ 0q�60   �� 5+       �@�Cursor$
 Hand��6'   �� 5,       � �� 5-      �� 5.      " ��6&   .R�5/      �� #��z��6!   $ 15��50   !   ���#4E8FC2$��1q�52   !    53        54   "   $	 3Q�63   $	
 330Q�6C   $ #4E8FC2��6S   $	 0.5Q�6c   $ 10Q�6q   ._�56      �� $��Center=�6/   $��Center=�6L   57       58      P� 59       �@J�IsMouseOver$
 True��6=   . �5:   "   ��  � �� 5;   &   �� 5<   *   $	 0,0&�6>   $	 0,1&�6O   .��5=   .   � $ #3F6AD8��6;   $ 0Q�6K   � 5>   .   $ #2A4494��6;   $ 1Q�6K   5?   +   5A   #   5B      P� 5C       � ��$
 True��6;   . �5D   "   ��  � �� 5E   &   �� 5F   *   $	 0,0&�6>   $	 0,1&�6O   .��5G   .   � $ #1C3F60��6;   $ 0Q�6K   � 5H   .   $ #2A4494��6;   $ 1Q�6K   5I   +   5K   #   �� 5L   "    � z� 5M   &   �� 5N   *   $
 0.98Q�69   $
 0.98Q�6G   5P   #   5Q      5S      5U      5V      5W      .��5Y      �  � ColumnDefinitions 5Z   
   �� 5[      $	��250 �6   �� 5\      $��* �6   �� 5]      $	��300 �6   .+�5a   
   �� $��0��6   #��[  6!    J� Opacity$
 0.85Q�6M   ._�5b      ]� $
��True��6   .+�5c      �� $	��Top=�6   $��
CONTINENTS��61   $��24(�6C   #��[ 6Q    �� TextAlignment$ Center=�5d      $��	0,20,0,20q�63   �� 5f      -   5        $��Bottom=�5f      $��BACK TO MENU��61   $��40��6H   $��20,20,20,20q�6T   #��[ 5g      6B   �� 5i      $
��Auto=�6   .��5j      �� -   5         ��Name$ 
labelStack��5j   !   $��10q�65   5k      5l      5m      �� 5p   
   $��1��6   ���#4E8FC2$��1,0,1,0q�67   ._�5q      &   HelixToolkit.Wpf.HelixViewport3D  -   5          Name$ viewport��5q   +      ZoomExtentsWhenLoaded$ False��5r   *      ShowViewCube$ False��5s   *    J� IsManipulationEnabled$ False��5t   *      IsMoveEnabled$ False��5u   *      Children. 5v        HelixToolkit.Wpf.SunLight  g� 5w      -   5        
globeModel��5w       5x      5y      �� 5|   
   $��2��6   #��[  6!   $
 0.85Q�6M   ._�5}      ]� $
��True��6   .+�5~      �� $	��Top=�6   $��CONTINENT INFO��61   $��24(�6G   #��[ 6U   $ Center=�5      $��	0,20,0,20q�63   �� 5�      -   5          ��Name$  exploreButton��5�      $��Bottom=�60   $��EXPLORE��6H   $��40��6Z   $��20,20,20,20q�6f   #��[ 5�      ${�False��6^   �� 5�      $��15q�6   .+�5�      �� -   5        ! ��Name$! continentNameText��5�       $��22(�6;   $
��Bold"�6I   ���#E9F2F9$ Center=�5�       $��0,0,0,15q�67   �� 5�      $	��300��6#   $
��Auto=�60   .��5�      �� -   5        $! continentDescriptionText��5�   $   $��14(�6F   ���#E9F2F9$
��Wrap=�5�   $   $��5q�68   5�      5�      5�      5�      5�      5�      
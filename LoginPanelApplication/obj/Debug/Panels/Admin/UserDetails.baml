   M S B A M L   `   `   `  ����P  LLoginPanelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:LoginPanelApplication.Converters LoginPanelApplication.Converters  5   	   .    (LoginPanelApplication.Panels.UserDetails   5      H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   D	converter.clr-namespace:LoginPanelApplication.Converters     5   	     � Title$  User Details��5	   	   $	��600��6   $	��900��6+    � WindowStartupLocation$ CenterScreen=�67   
���   �(  "MahApps.Metro.Controls.MetroWindow   WindowTitleBrush
 ����� � 	Resources 5
      >  8LoginPanelApplication.Converters.BoolToUserRoleConverter   BoolToUserRoleConverter@  :LoginPanelApplication.Converters.BoolToUserStatusConverter  BoolToUserStatusConverter8  2LoginPanelApplication.Converters.DateTimeConverter  DateTimeConverter8  2LoginPanelApplication.Converters.TimeSpanConverter  TimeSpanConverter��%t   &	        &	      &	 &     &	 9       5   
   6,     5   
   6.     5   
   6&     5   
   6&   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��40* �6   �� 5      $��1* �6   �� 5      $	��60* �6   .+�5   
   �� -   5         ��Name$ StackPanelDetails��5   !   $��10q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10q�62   .+�5      �� $	��100��6   .��6$   	UserId:6-   �� 5      $	��150��6   ��6(   ��  �� Path$ UserID� �� Mode$ OneWay=�5      �� 5      $ 
Horizontal=�6   $��10q�62   .+�5      �� $	��100��6   .��6$   Login:6,   �� 5      $	��150��6   ��6(   �� $ Login�$ OneWay=�5       �� 5!      $ 
Horizontal=�6   $��10q�62   .+�5"      �� $	��100��6   .��6$   	Password:6/   �� 5#      -   5        	 ��Name$	 txtPassword��5#   ,   
 �� 
IsReadOnly
 . $	��150��6A   ��6M   �� $ Password�$ OneWay=��� 5$      -   5         ��Name$ btnChangePassword��5$      .��6T   Change6\   5%      �� 5&      $ 
Horizontal=�6   $��10q�62   .+�5'      �� $	��100��6   .��6%   Name:6,   �� 5(      $	��150��6   ��6(   �� $
 Name�$ OneWay=�5)      �� 5*      $ 
Horizontal=�6   $��10q�62   .+�5+      �� $	��100��6   .��6%   
Last Name:61   �� 5,      $	��150��6   ��6(   �� $ LastName�$ OneWay=�5-      �� 5.      $ 
Horizontal=�6   $��10q�62   .+�5/      �� $	��100��6   .��6%   Role:6,   �� 50      $	��150��6   ��6(   �� $
 Role�$ OneWay=� �� 	Converter# [  51      �� 52      $ 
Horizontal=�6   $��10q�62   .+�53      �� $	��100��6   .��6%   	Hired on:60   �� 54      $	��150��6   ��6(   �� $ DateOfEmployment�$ OneWay=�55      �� 56      $ 
Horizontal=�6   $��10q�62   .+�57      �� $	��100��6   .��6$   Status6,   �� 58      -   5         ��Name$ txtblok��58      ��6-   �� $ Status�# [ �� 59      -   5        $ btnBlock��59   +   $��10,0,0,0q�6   .��6T   Block the user6d   5:      �� 5;      -   5        $
 Back��5;      .��66   Back6<   5<      �� 5=   
   $��1��6   $ 
Horizontal=�6%   .+�5>      �� $
��auto��6   $��2��6&   
�������6?   5?      �� 5@   
   $ Vertical=�6   $��2��6,   $��10q�6<   .+�5C      �� $ 
Horizontal=�6   .+�5D      �� -   5         ��Name$ ImageSource��5D      $	��150��6-   $	��150��6=   $��Center=�6L   $��5q�6g   ��6r   ��   ImageConverter# [ 6�   �� 5E      $��	30,0, 0,0q�6   .+�5F      �� -   5        $ btnDailyRaport��5F      $��10q�6R   .��6_   Daily raport6m   �� 5G      -	   5        $ btnMonthlyRaport��5G      $��10q�6W   .��6c   Monthly raport6s   5H      5I      &   System.Windows.Controls.DataGrid  5J      -
   5          Name$ UserDataGrid��5J         AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .  $   HorizontalScrollBarVisibility$ Disabled=�5P         
IsReadOnly .    Columns 5S      0  *System.Windows.Controls.DataGridTextColumn  5T      ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$	 100	 6-      Header$ User Id��69   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6J   �� $ UserID�   HeaderStyle 5U      �� 5V       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5W   "   ��  �� Property � �� �� Value$
 Bold"�6?   5X   #   5Z        5\      $	 200	 6-   $
 Date��69    6G   �� $ 	LoginDate�# [  5]      �� 5^      "  6$   .�5_   "   ��  � ��$
 Bold"�6?   5`      5b        5m      $	 200	 6)   $ Hours��65    6D   �� $ Hours�# [  5n      �� 5o      "  6$   .�5p   "   ��  � ��$
 Bold"�6?   5q      5s      5v      �� 5w      -   5        $ txtBlockRaport��5w      67   5y      5z      5{      
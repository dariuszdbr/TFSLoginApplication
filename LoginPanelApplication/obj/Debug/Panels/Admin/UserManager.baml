   M S B A M L   `   `   `  ����P  LLoginPanelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:LoginPanelApplication.Converters LoginPanelApplication.Converters  5      .    (LoginPanelApplication.Panels.UserManager   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      D	converter.clr-namespace:LoginPanelApplication.Converters     5      -   5          N�Name$  UserMgr��5   H    N� 
Background
 ����� N� Title$ UserManager��5	       � 	Resources 5      >  8LoginPanelApplication.Converters.BoolToUserRoleConverter   BoolToUserRoleConverter@  :LoginPanelApplication.Converters.BoolToUserStatusConverter  BoolToUserStatusConverter��%:   &	        &	        5   
   6,     5   
   6.   5      .��5      �� $��20q�6   
�������._�5   
   � .+�5      �� $��0,0,0,0q�6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridManager��5      $��10q�5      $	��370��6*      AutoGenerateColumns .     CanUserResizeRows .     CanUserAddRows .     CanUserDeleteRows .  	   CanUserReorderColumns	 .  
   FrozenColumnCount$
 2��5         
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6-      Header$ User ID��6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   ��  �� Path$ UserID�   HeaderStyle 5"      �� 5#   "    �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6(   .�5$   &   ��  �� Property � �� �� Value$
 Bold"�6C   5%   #   5'      4	  .System.Windows.Controls.DataGridTemplateColumn	  5)      $ Image��61      
IsReadOnly .  5*      �� 5+   "   "  6(   .�5,   &   ��  � ��$
 Bold"�6C   5-   #    	  CellTemplate 5/      �� 50   "   .R�51   &   �� $��50��6w   $��50��6�   $��Center=�6�   ��6,   �� $ ImageId�  ImageConverter �� 	Converter# [ 53   '   54   #   56        58      $
 Auto 6.   $
 Name��6;    6I   �� $
 Name� 59      �� 5:   "   "  6(   .�5;   &   ��  � ��$
 Bold"�6C   5<   #   5>        5@      $	 100 6-   $ 	Last Name��69    6L   �� $ LastName� 5A      �� 5B   "   "  6(   .�5C   &   ��  � ��$
 Bold"�6C   5D   #   5F        5H      $	 100 6-   $ Login��69    6H   �� $ Login� 5I      �� 5J   "   "  6(   .�5K   &   ��  � ��$
 Bold"�6C   5L   #   5N        5P      $	 100 6-   $ Password��69    6K   �� $ Password� 5Q      �� 5R   "   "  6(   .�5S   &   ��  � ��$
 Bold"�6C   5T   #   5V        5X      $ 80 6-   $
 Role��68    6F   �� $
 Role�# [   5Y      �� 5Z   "   "  6(   .�5[   &   ��  � ��$
 Bold"�6C   5\   #   5^        5`      $	 130 6-   $ Last login date��69    6R   �� $ In� 5a      �� 5b   "   "  6(   .�5c   &   ��  � ��$
 Bold"�6C   5d   #   5f        5h      $	 130 6-   $ Last logout date��69    6S   �� $	 Out� 5i      �� 5j   "   "  6(   .�5k   &   ��  � ��$
 Bold"�6C   5l   #   5n        5p      $	 130 6-   $ Date of employment��69    6U   �� $ DateOfEmployment� 5q      �� 5r   "   "  6(   .�5s   &   ��  � ��$
 Bold"�6C   5t   #   5v        5x      $ Status��6-    6=   �� $ Status�# [  5y      �� 5z   "   "  6(   .�5{   &   ��  � ��$
 Bold"�6C   5|   #   5~      ��5�      �� 5�      )
  #System.Windows.Controls.DataGridRow" 
 6    .�5�      ��  � ��
 �����6;    �� Triggers 5�      ]� 5�   "    ]� 
Conditions 5�   &   �� 5�   *    �� Property �@
 
IsSelected �� Value . 6J   �� 5�   *    �@J�	IsFocused . 6I   ./�5�   &   ��  � ��$ 15(�6A   �� 5�   &    � ��$
 Bold"�6C   5�   #   5�      5�      �� 5�      -   5         ��Name$ 
btnAddUser��5�   -   $��
20,0,20,10q�6   .��6Z   
Add User6d   �� 5�      -   5        $ 
btnDetails��5�   -   $��
20,0,20,10q�6   .��6Z   Check User Attendace6p   �� 5�      -   5        $ btnDeleteUser��5�   -   $��
20,0,20,10q�6   .��6`   Delete Selected User6v   5�      �� 5�      -   5        $ btnBack��5�   Y   $��Bottom=�6   $
��Left=�60   $��	20,0,20,0q�6F   .��6�   Back to Admin panel6�   5�      5�      5�      
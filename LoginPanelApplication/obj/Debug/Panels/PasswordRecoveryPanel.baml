   M S B A M L   `   `   `  ����P  LLoginPanelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LoginPanelApplication.PanelsLoginPanelApplication.Panels  5      7    1LoginPanelApplication.Panels.PasswordRecveryPanel   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:LoginPanelApplication.Panels     5        N� 
Background
  ����� N� Title$ RestorePassword��5	      .��5      �� $��20q�6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $	��100��6   .��6    
User Login6,   �� 5      -   5         ��Name$ txtUserLogin��5      $	��150��6,   6:   5      �� 5   
   $ 
Horizontal=�6   .+�5      �� $	��100��6   .��6    	User Name6+   �� 5      -   5        $ txtName��5      $	��150��6'   65   5      �� 5   
   $ 
Horizontal=�6   .+�5      �� $	��100��6   .��6    	Last Name6+   �� 5      -   5        $ txtLastName��5      $	��150��6+   69   5      �� 5   
   -   5         ��Name$ btnFindPassword��5      $��	0,10,0,10q�6H   .��6[   Find Password6j   �� 5   
   .+�5       &   System.Windows.Controls.DataGrid  -   5          Name$ dgRestorePassword��5          AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  	   CanUserReorderColumns	 .  
   CanUserResizeColumns
 .     CanUserSortColumns .     Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 92 6)      Header$ Userd Id��64   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   ��  �� Path$ UserID�  5*      $ 92 6)   $ 	User Name��64    6G   �� $
 Name�  5+      $ 92 6)   $ 	Last Name��64    6G   �� $ LastName�  5,      $ 92 6)   $ Login��64    6C   �� $ Login�  5-      $ 92 6)   $ Password��64    6F   �� $ Password�5/      51      �� 52   
   -   5        $ btnBack��52   #   $��0,50,0,0q�6   .��6J   Back6P   53      54      
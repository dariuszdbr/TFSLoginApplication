   M S B A M L   `   `   `  ����P  LLoginPanelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LoginPanelApplication.PanelsLoginPanelApplication.Panels  5   	   1    +LoginPanelApplication.Panels.ChangePassword   5      H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LoginPanelApplication.Panels     5   	     � Title$  Set new password��5	   	   $	��600��6#   $	��900��60    � WindowStartupLocation$ CenterScreen=�6<   (  "MahApps.Metro.Controls.MetroWindow   WindowTitleBrush
 �����.��5
      � $��
0,30,30,30q�6   $��center=�6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� $��Bottom=�6    �� Orientation$ 
Horizontal=�60   .+�5      �� $	��100��6   $��Black"�6    $��New password:��63   �� 5      -   5         ��Name$ txtPassword��5   *   
���   �$��Stretch=�6?   $	��420��6]   6l   5      �� 5   
   $��1��6   $	��Top=�6#   $ 
Horizontal=�6<   $��0,20,0,0q�6U   .+�5      �� $	��100��6   $��Black"�6    .��64   	Repeat:6>   �� 5      -   5        $ 	txtRepeat��5   *   
���   �$	��420��6=   $��Stretch=�6I   6i   5      �� 5   
   -   5         ��Name$ btmSetPassword��5      $��1��6*   $��Center=�67   .��6o   Set new password6�   5      5      
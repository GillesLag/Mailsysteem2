   M S B A M L   `   `   `  ����J  FMailsysteem_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Mailsysteem_WPFMailsysteem_WPF  5   	   '    !Mailsysteem_WPF.NieuweVergadering   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Mailsysteem_WPF     5   	     � Title$  NieuweVergadering��5   	   $	��250��6#   $	��800��60   $	��250��6<   $	��250��6L   .��5	      � .+�5
   
   �� $��0��6   $��1q�6   
���   �6H   � 5   
   $��0��6    � ColumnDefinitions 5      �� 5      $	��120 �6#   �� 5      .+�5      �� -   5        $��btnAanmaken��5      $��Aanmaken��6(   $��0��6;   $��80��6K   $��80��6V   6b   �� 5      $��1��6   $��0,20,0,0q�6)   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�66   .+�5      �� -   5        $��lblTitel��5      $��Titel:��6,   $��Right=�6=   $	��125��6`   �� 5      -   5         ��Name$ tbTitelTekst��5      $��Center=�64   $	��500��6V   $��20��6b   5      �� 5      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�5      �� -   5        $��lblDeelnemers��5      $��Deelnemers:��61   $��Right=�6G   $	��125��6j   �� 5      -   5        $ tbDeelnemeersTekst��5      $��Center=�6:   $	��500��6\   $��20��6h   5      �� 5      $ 
Horizontal=�6   $��0,0,0,5q�66   .+�5      �� -   5        $��lblDatum��5      $��Datum:��6,   $��Right=�6=   $	��125��6`   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpDatum��5   !   $��Center=�62   5       �� 5!      $ 
Horizontal=�6   $��0,0,0,5q�66   .+�5"      �� -   5        $��lblBegintijd��5"      $��
Begintijd:��60   $��Right=�6E   $	��125��6h   �� 5#      -	   5        $ tbBegintijd��5#      $��Center=�63   $	��125��6U   �� 5$      -
   5        $��
lblFormat1��5$      $��Timeformat = 00:00��6.   
�������6K   5%      �� 5&      $ 
Horizontal=�6   $��0,0,0,5q�66   .+�5'      �� -   5        $��lblEindtijd��5'      $��	Eindtijd:��6/   $��Right=�6C   $	��125��6f   �� 5(      -   5        $ 
tbEindtijd��5(      $��Center=�62   $��25��6T   $	��125��6`   �� 5)      -   5        $��
lblFormat2��5)      $��Timeformat = 00:00��6.   
�������6K   5*      �� 5+      $ 
Horizontal=�6   .+�5,      �� -   5        $��
lblLocatie��5,      $��Locatie:��6.   $��Right=�6A   $	��125��6d   �� 5-      -   5        $ tbLocatieTekst��5-      $��Center=�66   $	��500��6X   $��20��6d   5.      5/      50      51      52      
   M S B A M L   `   `   `  ����F  BRouletteApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:RouletteApp.ViewModelRouletteApp.ViewModel  5   	   5clr-namespace:RouletteApp.UtilsRouletteApp.Utils  5   	       RouletteApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2vm#clr-namespace:RouletteApp.ViewModel     5   	   1utilsclr-namespace:RouletteApp.Utils     5   	     � Title$  Roulette Results��5   	   $	��600��6"   $	��800��6/   $	��600��5   	   $	��800��6    � 	Resources 5	      -   'RouletteApp.Utils.OddEvenColorConverter   OddEvenColorConverter   BooleanToVisibilityConverter��%:   &	        &	        5
   
   6&   �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��5q�6    �� Padding$ 5q�6)   
�������
�������$��1q�6a   y�5      �� *IsNotificationVisible+ �� 	Converter# [ ._�5      �� .+�5      �� $��Notification��6   $
��Bold"�60   �� 5      $
��Wrap=�6>   ��6   �� *NotificationText+6>   5      5      �� 5   
   $��1��6   $��5q�6?   ��6   �� *	Results+��5      �� 5      .R�5       ��  �� Orientation$ Vertical=�6!   .+�5!      �� ��6$   �� *
multiplier+ �� StringFormat$
 x{0}���� 5"      ��6$   �� *
position+��6>   �� *
position+# [  5#      5$      5&      �� 5(   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5)      �� $��Add random result��6   $��5q�6V   ��61   �� *AddResultCommand+6V   �� 5*      $��Show notification��6   $��5q�6]   ��61   �� *ShowNotificationCommand+6]   5+      �� 5-   
   $��3��6   $��5q�6   $ 5q�6)   
�������$��1q�6H   ._�5.      �  � ColumnDefinitions 5/      �� 50      $��* �6'   �� 51      $��* �6'   .+�53      �� $��0��6   $��5q�6-   .+�54      �� $��Active Players��6    $
��Bold"�66   �� 55      $��16(�6C   ��6    �� *ActivePlayerCount+6C   56      �� 57      $��1��6   $��5q�6-   .+�58      �� $��Biggest Multiplier��6    $
��Bold"�6:   �� 59      $��16(�6V   ��6    �� *BiggestMultiplier+$
 x{0}��6V   5:      5;      5<      5=      5>      
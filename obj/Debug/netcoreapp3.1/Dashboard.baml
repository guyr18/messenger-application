   M S B A M L   `   `   `  ����H  Dmessenger_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:messenger_appmessenger_app  5   	       messenger_app.Dashboard   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:messenger_app     5   	     � Title$  	Dashboard��5   	    � 
ResizeMode$ NoResize=�6   $	��450��61   $	��800��6>   
�������.��5	      �  � RowDefinitions 5
   
   �� 5      $
��335* �6   �� 5      $	��99* �6   .+�5   
   �� $��My Messages��6   $��282,112,329,178q�6&   $��14(�6?   $
��Bold"�6M    J� RenderTransformOrigin$ 0.508,0.206&�6_    K� ZIndex$ 1��6�   �� 5   
   $��1q�6   ���	#FFC5C5C5$��	263,0,0,0q�6=   $��2��6P   ._�5      �� $
��Auto=�6   .��5      �� -   5         ��Name$ myStackPanel��5      $��
0,160,0,45q�63   ���	#FFBBBBBB$	��272��6^   5      5      �� 5   
   -   5         ��Name$ textWelcome��5      $��Welcome, <x>��6%   $
��Left=�6B   $��	31,44,0,0q�6]   $	��Top=�6p   $	��193��6�   $
��Bold"�6�   $��22(�6�   
���   �6�   �� 5   
   -   5        $ textMessageInfo��5      $��Label��6)   $
��Left=�69   $��
282,26,0,0q�6T   $	��Top=�6h   �� 5   
   -   5         ��Name$ 
textSearch��5      $��12,0,0,0q�6&   $��center=�69   $��<enter an email address>��6[    �� TextWrapping$
 Wrap=�6~   $��291,57,149,228q�6�   ���	#FF8585856�   �� 5   
   -   5        	 ��Name$	 buttonMessages��5      $��Messages��6)   $��-4,100,542,178q�6<   $��Segoe UI)�6T   ���	#FFBDBBBB
����������	#FF09A7E2$��18(�6�   $��Normal"�6�   �� 5   
   -   5        $	 buttonSearch��5      $��Search��6'   $
��Left=�68   $��
673,57,0,0q�6S   $	��Top=�6g   $��33��6   $��77��6�   6�   �� 5   
   -   5        $	 
buttonBack��5      $��Logout��6%   $
��Left=�66   $��
56,217,0,0q�6Q   $	��Top=�6e   $��32��6}   $	��144��6�   ���	#FF0A7AFF$
��Bold"�6�   $��16(�6�   
�������
�������6�   5      5      
   M S B A M L   `   `   `  ����H  Dmessenger_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:messenger_appmessenger_app  5   	        messenger_app.Conversation   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:messenger_app     5   	     � Title$  Conversation��5   	    � 
ResizeMode$ NoResize=�6   $	��450��64   $	��800��6A   
�������.��5	      �  � RowDefinitions 5
   
   �� 5      $
��335* �6   �� 5      $	��99* �6   .+�5   
   �� $��1q�6   ���	#FFC5C5C5$��	263,0,0,0q�6=   $��2��6P   ._�5      �� $
��Auto=�6   .��5      �� -   5         ��Name$ chatStackPanel��5      $��	0,73,0,61q�65   ���	#FFCCCCCC.+�5      �� -   5         ��Name$ textInfo��5      $��Label��6.   $��Center=�6>   $
��Bold"�6[   5      5      5      �� 5   
   -   5        $ textRecipient��5      $��	recipient��6'   $��284,7,315,271q�6;   $
��Bold"�6R   $��16(�6d   �� 5   
   -   5        $ textWelcome��5      $��Welcome, <x>��6%   $
��Left=�6B   $��	31,44,0,0q�6]   $	��Top=�6p   $	��193��6�   $
��Bold"�6�   $��22(�6�   
���   �6�   �� 5   
   -   5         ��Name$ buttonMessages��5      $��Messages��6)   $��-4,100,542,178q�6<   $��Segoe UI)�6T   ���	#FFBDBBBB
����������	#FF09A7E2$��18(�6�   $��Normal"�6�   �� 5   
   -   5         ��Name$ textMessage��5      $��12,0,0,0q�6'   $��Center=�6:   $
��Left=�6\   $��
284,50,0,0q�6w   $��Type a message..��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��327��6�   ���	#FF919191$��29��6�   $��1��6�   6&  �� 5   
   -   5        $ 
buttonSend��5      $
��Send��6%   $��645,45,21,15q�64   $��Segoe UI)�6J   ���	#FFBDBBBB
�������$��18(�6�   
���   �$��1��6�    J� RenderTransformOrigin$ 0.448,1&�6�   6�   5      5      
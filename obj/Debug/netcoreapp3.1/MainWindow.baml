   M S B A M L   `   `   `  ����H  Dmessenger_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:messenger_appmessenger_app  5   	       messenger_app.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:messenger_app     5   	     � Title$  
MainWindow��5   	    � 
ResizeMode$ NoResize=�6   $	��450��62   $	��800��6?   .��5	      � .+�5
   
   �� $��1q�6   ���	#FF2C82EA$��180,117,180,170q�6=    �� CornerRadius$ 8,8,8,8��6V   �� 5   
   $��Messenger App��6   $��Center=�6(   $	��Top=�6E   $��20(�6]   $
��Bold"�6k   $��0,60,0,0q�6}   �� 5   
   -   5         ��Name$ 	textEmail��5      ���#232323$
��Left=�6:   $��12,0,0,0q�6U   $��example@yourdomain.com��6h    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��Center=�6�   $	��200��6�   $��29��6�    �� TextAlignment$
 Left=�6�   $��310,143,0,0q�6  65  G� 5   
   -   5         G�Name$ textPassword��5      ���#232323$
��Left=�6A   $��12,0,0,0q�6\    G� Password$ <enter your password>��6o   $��Center=�6�   $��Center=�6�   $	��200��6�   $��29��6�   $��	310,0,0,0q�6�   6  �� 5   
   $��Email Address��6   $
��Left=�6(   $��201,143,0,0q�6C   $	��Top=�6X   $
��Bold"�6p   �� 5   
   $��Password��6   $
��Left=�6#   $	��Top=�6>   $
��Bold"�6V   $��227,191,0,0q�6h   �� 5   
   -   5        $��Login��5      $��Center=�6!   $��	0,281,0,0q�6>   $	��Top=�6Q   $��32��6i   $	��144��6u   ���	#FF0A7AFF$
��Bold"�6�   $��16(�6�   
�������
�������6�   �� 5   
   $��Need an account?��6   $
��Left=�6+   $��310,327,0,0q�6F   $	��Top=�6[   �� 5   
   -   5         ��Name$ linkRegister��5      $��Click here.��6&   $
��Left=�6<   $��415,327,0,0q�6W   $	��Top=�6l   ���	#FF3DA4DE6�   �� 5   
   -   5        $ 	textError��5      $(��"Invalid email/password combination��6#   $
��Left=�6P   $��290,246,0,0q�6k   $	��Top=�6�   
����������	#FFFF0101$
��Bold"�6�   	 J� RenderTransformOrigin$	 0.526,-0.234&�6�   5      5      
   M S B A M L   `   `   `  ����H  Dmessenger_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:messenger_appmessenger_app  5   	       messenger_app.Register   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:messenger_app     5   	     � Title$  Register��5   	   $	��450��6    � 
ResizeMode$ NoResize=�6'   $	��800��6=   .��5	      � .+�5
   
   �� $��User Registration��6   $��Center=�6,   $	��Top=�6I   $��20(�6a   $
��Bold"�6o   $��0,24,0,0q�6�   �� 5   
   -   5         ��Name$ 	textEmail��5      ���#232323$
��Left=�6:   $��12,0,0,0q�6U   $��example@yourdomain.com��6h    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��Center=�6�   $	��200��6�   $��29��6�    �� TextAlignment$
 Left=�6�   $��310,115,0,0q�6  65  �� 5   
   $��Email Address��6   $
��Left=�6(   $��201,115,0,0q�6C   $	��Top=�6X   $
��Bold"�6p   G� 5   
   -   5         G�Name$ textPassword��5      ���#232323$
��Left=�6A   $��12,0,0,0q�6\    G� Password$ <enter your password>��6o   $	��Top=�6�   $��Center=�6�   $	��200��6�   $��29��6�   $��310,175,0,0q�6�   69  �� 5   
   $��Password��6   $
��Left=�6#   $	��Top=�6>   $
��Bold"�6V   $��227,163,0,0q�6h   G� 5   
   -   5        $ textPasswordConfirm��5      ���#232323$
��Left=�6H   $��12,0,0,0q�6c   $ <enter your password>��6v   $	��Top=�6�   $��Center=�6�   $	��200��6�   $��29��6�   $��310,240,0,0q�6�   6N  �� 5   
   $��Gender
��6   $
��Left=�6+   $	��Top=�6F   $
��Bold"�6^   $��234,298,0,0q�6p   	� 5   
   -   5         	�Name$ maleRB��5      $
��Male��6&   $
��Left=�65   $��310,304,0,0q�6P   $	��Top=�6e    d� 	IsChecked$
 TrueY�6}   6�   �� 5   
   $��Confirm Password��6   $
��Left=�6+   $	��Top=�6F   $
��Bold"�6^   $��178,237,0,0q�6p   	� 5   
   -   5        $ femaleRB��5      $��Female��6(   $
��Left=�69   $��400,304,0,0q�6T   $	��Top=�6i   6�   �� 5   
   -   5        	 ��Name$	 buttonRegister��5      $��Register��6)   $��Center=�6<   $��	0,349,0,0q�6Y   $	��Top=�6l   $��32��6�   $	��144��6�   ���	#FF0A7AFF$
��Bold"�6�   $��16(�6�   
�������
�������6�   �� 5   
   -   5        
 ��Name$
 textEmailError��5      $��textEmailError��6(   $
��Left=�6A   $��530,117,0,0q�6\   $	��Top=�6q   $
��Bold"�6�   
�������6�   �� 5   
   -   5        $
 textPasswordError��5      $��textPasswordError��6+   $
��Left=�6G   $��530,176,0,0q�6b   $	��Top=�6w   $
��Bold"�6�   
�������6�   �� 5   
   -	   5        $ textFullName��5      ���#232323$
��Left=�6=   $��12,0,0,0q�6X   $��<first_name> <last_name>��6k   $
 Wrap=�6�   $	��Top=�6�   $��Center=�6�   $	��200��6�   $��29��6�   $
 Left=�6�   $��
310,66,0,0q�6  6l  �� 5   
   $/��)Full Name (seperate with space; John Doe)��6   $
��Left=�6D   $��	41,69,0,0q�6_   $	��Top=�6r   $
��Bold"�6�   �� 5   
   -
   5        $
 textFullNameError��5      $��textFullNameError��6+   $
��Left=�6G   $��
530,69,0,0q�6b   $	��Top=�6v   $
��Bold"�6�   
�������6�   �� 5   
   -   5        $	 
buttonBack��5      $��Go Back��6%   $
��Left=�67   $��	41,20,0,0q�6R   $	��Top=�6e   $��32��6}   $	��144��6�   ���	#FF0A7AFF$
��Bold"�6�   $��16(�6�   
�������
�������6�   5      5      
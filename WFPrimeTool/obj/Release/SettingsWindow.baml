   M S B A M L   `   `   `  ���� H  DWF Prime Tool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WFPrimeToolWFPrimeTool       WFPrimeTool.SettingsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:WFPrimeTool     -     � Title$  SettingsWindow��$��304.5��$��411.5�� � 
ResizeMode$ NoResize=� � Topmost .  � WindowStartupLocation$ CenterOwner=����	#FF37393A���	#FF515151.��� .+��� -    ��Name$ checkBox��$��Max Scan Retry's: ��$
��Left=�$��	10,10,0,0q�$	��Top=�$��40��$	��222��$��20(�$
��Bold"����	#FF5EACA1 � ToolTip$\ VSets the amount of times it should try before passing on to the next item for scanning���� -    ��Name$ slider��$
��Left=�{�.  $��
252,13,0,0q�$	��Top=�$	��135��$��24�� � Minimum$ 4Q� � Maximum$ 19Q��� -   	 ��Name$	 button��$��Load Filter��$
��Left=�$��
10,235,0,0q�$	��Top=�$	��129��$��30�����	#FF5EACA1$
��Bold"����	#4C000000$> 8Loads the filter.txt if you have made any changes to it.���� -   $ 	checkBox1��$��Auto Hide on Scan��$
��Left=�$��	10,50,0,0q�$	��Top=�$��40��$	��200��$��20(�$
��Bold"����	#FF5EACA1$O IHides the WF Prime Tool Window while Scanning the items in the inventory.���� -   $	 button_Copy��$��Check Allignment��$
��Left=�$��
10,200,0,0q�$	��Top=�$	��129��$��30�����	#FF5EACA1$
��Bold"����	#4C000000$H BCheck the allignment for scanning Prime Parts from your Inventory.���� -   
 ��Name$
 image��$
��Left=�$��67��$��144,165,0,0q�$	��Top=�$	��136��$ Prime Part Name Location���� -   $
 image2��$
��Left=�$��49��$��312,165,0,0q�$	��Top=�$��66��$ Prime Part Count Location���� -	   $ 	checkBox2��$��Disable Error Logging�� d� 	IsChecked$
 trueY�$
��Left=�$��	10,90,0,0q�$	��Top=�$��40��$	��230��$��20(�$
��Bold"����	#FF5EACA1$% Disables/Enabled Error Logging.���� -
   $ 
laststatus��$��Enable Last Status��$ falseY�$
��Left=�$��
10,130,0,0q�$	��Top=�$��40��$	��230��$��20(�$
��Bold"����	#FF5EACA1$\ VDisables/Enabled Using the last profile status used if disabled defaults to invisible.���� -   $	 button_Copy1��$��Custom Scan Settings��$
��Left=�$��
10,165,0,0q�$	��Top=�$	��129��$��30�����	#FF5EACA1$
��Bold"����	#4C000000$) #Scan/Filter Customization Settings.��
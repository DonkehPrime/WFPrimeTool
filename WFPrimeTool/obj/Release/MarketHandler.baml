   M S B A M L   `   `   `  ���� H  DWF Prime Tool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WFPrimeToolWFPrimeTool      WFPrimeTool.MarketHandler   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:WFPrimeTool     -     � Title$  Market Management��$��464.167��$	��800�����	#FF1F2427���	#336C6B6B � Topmost .  � WindowStartupLocation$ CenterOwner=�.��� ���	#4C6F7074.+��� -    ��Name$ label��$��Sell Orders�����	#FF5EACA1$
��Left=�$��	154,5,0,0q�$	��Top=�$	��118��$��37��$��20(�$
��Bold"��� -   $ 
label_Copy��$��
Buy Orders�����	#FF5EACA1$
��Left=�$��	532,5,0,0q�$	��Top=�$	��118��$��37��$��20(�$
��Bold"��� -    ��Name$ soldb��$
��Sold��$
��Left=�$��180,368,0,0q�$	��Top=�$��80��$��37�����	#FF5EACA1$
��Bold"� � ToolTip$O ISelect a item in the list and then click this button if you have sold it.�� J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$ 1Q�   RenderingBias$ Quality=����� 	 �� Color#	 � ��
 �� Opacity$	
 0.2Q��� -   $ boughtb��$��Bought��$
��Left=�$��534,368,0,0q�$	��Top=�$��80��$��37�����	#FF5EACA1$
��Bold"�$Q KSelect a item in the list and then click this button if you have Bought it.��   $ 1Q�$ Quality=����� #	 � ��$	
 0.2Q��� -    ��Name$ selllist��$
��Left=�$	��310��$��	33,42,0,0q�$	��Top=�$	��354�����	#FF7A7A7A$��16(����	#FF5EACA1$
��Bold"����� *slist+ �� NotifyOnSourceUpdated .  �� View �� .P��� $
��name�� �� Width$	 200�� �� DisplayMemberBinding �� *name+�� $��x��$ 30�� �� *
quantity+�� $
��plat��$ 50�� �� *
platinum+�� $��visible��$ 70�� �� *	visible+�� $��id��$ 0�� �� *id+�� -   $ buylist��$
��Left=�$	��310��$��
408,42,0,0q�$	��Top=�$	��354�����	#FF7A7A7A$��16(����	#FF5EACA1$
��Bold"����� *blist+ �� .P��� $
��name��$	 200�� �� *name+�� $��x��$ 30�� �� *
quantity+�� $
��plat��$ 50�� �� *
platinum+�� $��visible��$ 70�� �� *	visible+�� $��id��$ 0�� �� *id+�� -   $ 
OrdersStat��$��Status: Invisible��$y�Visible=�$
��Left=�$��331,368,0,0q�$	��Top=�$	��134��$��37�����	#FF5EACA1$
��Bold"�$L FWarframe Market Status Management, Change your warframe market status.��   $ 1Q�$ Quality=����� #	 � ��$	
 0.2Q��� -	    ��Name$ ordbox�����	#FF252627$
��Left=�$y�Hidden=�$��56��$��265,358,0,0q�
���   �$	��Top=�$	��264���� -
    ��Name$ quantbox��$
��Left=�$��28��$y�Hidden=�$��271,373,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=����	#FF5EACA1$
��Bold"�$��34�����	#FF3C3838�� -   $ platbox��$
��Left=�$��28��$y�Hidden=�$��315,373,0,0q�$
 Wrap=�$�� ��$	��Top=����	#FF5EACA1$
��Bold"�$��40�����	#FF3C3838�� -   $ platlab��$��Plat:��$
��Left=�$y�Hidden=�$��309,353,0,0q����	#FF5EACA1$
��Bold"�$	��Top=�$��28���� -   $ quantlab��$��X:��$
��Left=�$y�Hidden=�$��267,353,0,0q����	#FF5EACA1$
��Bold"�$	��Top=�$��28���� -    ��Name$ checkBox��$
��Hide��$
��Left=�$y�Hidden=�$��362,380,0,0q����	#FF5EACA1$
��Bold"�$	��Top=��� -   $ hideb��$��Update Order��$y�Hidden=�$
��Left=�$��417,368,0,0q�$	��Top=�$��80��$��38�����	#FF5EACA1$
��Bold"�   $ 1Q�$ Quality=����� #	 � ��$	
 0.2Q��� -   $ 
hidebclose��$��X��$y�Hidden=�$
��Left=�$��502,368,0,0q�$	��Top=�$��22��$��38�����	#FF5EACA1$
��Bold"�   $ 1Q�$ Quality=����� #	 � ��$	
 0.2Q�
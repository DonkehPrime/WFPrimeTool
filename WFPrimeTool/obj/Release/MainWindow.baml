   M S B A M L   `   `   `  ����   WF Prime Tool    WFPrimeTool.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$7  1WFPrimeTool - Made By: RonkestDonk#7422 (Discord)��$��607.333��$��966.5�����	#FF1F2427���	#336C6B6B � 
ResizeMode$ NoResize=�.��� ���	#4C6F7074 � ColumnDefinitions �� $
��442* ��� $
��517* �.+��� -    ��Name$ button��$��Scan Prime Parts��$
��Left=�$��298,473,0,0q�$	��Top=�$	��174��$��38��$��20(�$
��Bold"����	#FF5EACA1 � ToolTip$) #Start/Stop Scanning Inventory Items��$��2�� J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$ 1Q�   RenderingBias$ Quality=�����  �� Color# � ��	 �� Opacity$		 0.2Q��� -   
 ��Name$
 textBox��$
��Left=�$	��455��$��	10,10,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��348��$��20(�$
��Bold"����	#4CBDBCBC���	#FF5EACA1�� -   $
 textBox2��$y�	Collapsed=�$
��Left=�$��42��$��
10,423,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��348��$��20(�$
��Bold"����	#4CBDBCBC���	#FF5EACA1�� -    ��Name$ image��$
��Left=�$y�	Collapsed=�$��74��$��
10,344,0,0q�$	��Top=�$	��170���� -   $ image2��$
��Left=�$y�	Collapsed=�$��74��$��185,344,0,0q�$	��Top=�$	��170���� -    ��Name$ 	ItemCount��$��ScanCount: ��$
��Left=�$��300,509,0,0q�$	��Top=�$��34��$��20(�$	��184��$
��Bold"����	#FF5EACA1$��2������ $ 	#00000000��$		 0.2Q��� -   $ TotItemCount��$��Total: ��$
��Left=�$��300,531,0,0q�$	��Top=�$��34��$��20(�$	��184��$
��Bold"����	#FF5EACA1$��2������ $ 	#00000000��$		 0.2Q��� -	   $ GetCount��$��
GetCount: ��$
��Left=�$��
64,509,0,0q�$	��Top=�$��50��$��20(�$	��174��$
��Bold"����	#FF5EACA1$��1������ $ 	#00000000��$		 0.2Q��� -
   $ button1��$��SortBy��$
��Left=�$��418,476,0,0q�$	��Top=�$��88��$��38��$��20(�$
��Bold"����	#FF5EACA1$��1��$F @Sort the item list By:  Quantity(X) -> Name -> Platinum -> Ducat��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -   $ button2��$��Get Price Data��$
��Left=�$��
48,473,0,0q�$	��Top=�$	��174��$��38��$��20(�$
��Bold"����	#FF5EACA1$6 0Collect Ducat/Platinum Data from warframe.market��$��1��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -    ��Name$ listBox��$y�Hidden=�$
��Left=�$	��455��$��
363,10,0,0q�$	��Top=�$	��585��$
��Bold"�$��20(����	#4CBDBCBC���	#FF5EACA1$��2���� -   $ button3��$��	ClearList��$
��Left=�$��418,522,0,0q�$	��Top=�$��88��$��38��$��20(�$
��Bold"����	#FF5EACA1$��1��$# Clears the List of all Items.��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -   $ button5��$��
listscheck��$
��Left=�$��
10,344,0,0q�$	��Top=�$��75��{�.  $y�Hidden=�	� -    	�Name$ 	openorder��$��
Open Order��$
��Left=�$��247,505,0,0q�$	��Top=�$	��154�����	#FF5EACA1���	#FFC7C1C1$��20(�$
��Bold"�$��1��$l fIf Enabled once you click one of the items in the list will take you to corresponding item order page.������ $ White��$		 0.5Q�	� -   $ copyname��$��	Copy Name��$
��Left=�$��247,476,0,0q�$	��Top=�$	��154�����	#FF5EACA1���	#FFC7C1C1$��20(�$
��Bold"�$��1��$q kIf Enabled items you select in the list will be set to your clipboard for easy searching in your inventory.������ $ White��$		 0.5Q�	� -   $ 
placeorder��$��Place Order��$y�Hidden=�$
��Left=�$��247,535,0,0q�$	��Top=�$	��154�����	#FF5EACA1���	#FFC7C1C1$��20(�$
��Bold"�$��1��$` ZIf Logged In allows you to select a item from the list to place a order to warframe.market������ $ White��$		 0.5Q��� -   $ button4_Copy��$��Settings��$
��Left=�$��
10,473,0,0q�$	��Top=�$��81��$��38�����	#FF5EACA1$
��Bold"�$ Settings Menu��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -   $ Contact��$��Discord��$
��Left=�$��100,473,0,0q�$	��Top=�$��69��$��38�����	#FF5EACA1$
��Bold"�$9 3Opens discord invite for help/questions/suggestions��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -    ��Name$ logwin�����	#FF1F2427$
��Left=�$	��210��$��	2,359,0,0q�
���   �$y�Hidden=�$	��Top=�$	��314���� -   $ maillab��$��Email Adress��$
��Left=�$��
10,359,0,0q�$	��Top=�$��39��$y�Hidden=�$	��330��$��20(�$
��Bold"����	#FF5EACA1�� -   $ passlab��$��Password��$
��Left=�$��
10,430,0,0q�$	��Top=�$��35��$y�Hidden=�$	��330��$��20(�$
��Bold"����	#FF5EACA1�� -   $
 mailbox��$
��Left=�$��39��$��
10,392,0,0q�$	��Top=�$	��296��$y�Hidden=����	#FFD5DEC0$��20(�$
��Bold"����	#FF5EACA1G� -    G�Name$ passbox��$
��Left=�$��
10,464,0,0q�$	��Top=�$	��296��$��38��$y�Hidden=����	#FFD5DEC0$��20(�$
��Bold"����	#FF5EACA1�� -   $ button6��$��	Login WFM��$
��Left=�$��
10,522,0,0q�$	��Top=�$��81��$��39�����	#FF5EACA1$
��Bold"�$: 4Login to Warframe Market to be able to Place Orders.��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -    ��Name$ savelog��$��Save Login(Test)��$
��Left=�$��159,522,0,0q�$y�Hidden=�$	��Top=�$	��148��$��31�����	#FF699990$
��Bold"�$��15(��� -   $ errorpan�����	#FF1F2427$
��Left=�$	��104��$��
22,392,0,0q�
���   �$	��Top=�$	��267��$y�Hidden=��� -   $ errorlab��$;��5Error: awefjmolauwefnwaefiu{}\niom428349fa\\nwl;ef//n��$
��Left=�$��
22,392,0,0q�$	��Top=�$	��267��$	��104��$��14(����	#FFC39B9B$y�Hidden=��� -   $ Orders��$��Orders��$
��Left=�$��100,522,0,0q�$y�Hidden=�$	��Top=�$��69��$��39�����	#FF5EACA1$
��Bold"�$' !Warframe Market Order Management.��   $ 1Q�$ Quality=����� # � ��$		 0.2Q��� -    ��Name$ itemview��$
��Left=�$	��455��$��
363,10,0,0q����	#4CBDBCBC$��20(����	#FF5EACA1$
��Bold"�$	��Top=�$	��587��$��2�� �� View �� .P��� $��x�� �� Width$ 30�� �� DisplayMemberBinding �� *
quantity+�� $
��name��$	 230�� �� *name+�� $
��plat��$ 50�� �� *
platinum+�� $	��duc��$ 50�� �� *ducats+
   M S B A M L   `   `   `  ����  StockExchangeGUI'    !StockExchangeGUI.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � Title$" Magnis Real Time Stock Datas��5   	   $	��410��6.   $	��450��6;    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6   .  (System.Windows.Interactivity.Interaction   Triggers 5
      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6%   �� *ExitCommand+5      .��5      � .+�5   
   �� $
��Left=�5      $��	11,16,0,0q�5      $	��Top=�5      $��16(�5      $��Sitka Small)�5      ��5      �� *ConnectionLabel+5      �� 5   
   $��222,60,60,0q�5      $��36��5      $	��Top=�5      $
��Bold"�5      $��Subscribe to the symbol��5      ��5      �� *SubscribedToTheSymbolCommand+{�5      �� *IsEnableSubscribedButton+5      �� 5   
   $��10,60,277,0q�5       $��36��5!      $	��Top=�5"      ��5#      ��  �� Path$ SymbolsCollection�	 �� SelectedItem	 5$      �� *SelectedSymbol+5%      �� 5&   
   .+�5'      ]� .+�5(      �� $��1,125,35,40q�5)      $
��Auto=�5*      $
��Auto=�5+      .��5,      &   System.Windows.Controls.DataGrid  
   
IsReadOnly
 .    AutoGenerateColumns .  $
��Auto��50      ��5.      �� *
StockDatas+   Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ 90	 6L      Header$ Symbol��6X   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 61   �� *Symbol+6X     53      $ 90	 6K   $ Price��6W    61   �� *Price+6W     54      $ 90	 6L   $ Volume��6X    61   �� *Volume+6X     55      $	 120	 6N   $ DateTime��6[    61   �� *
DateTime+6[   57      58      59      5:      5;      5<      
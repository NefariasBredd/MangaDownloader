   M S B A M L   `   `   `  ����J  FMangaDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MangaDownloader.HelpersMangaDownloader.Helpers  5   	   1clr-namespace:MangaDownloaderMangaDownloader  5   	   G(clr-namespace:MangaDownloader.ViewModelsMangaDownloader.ViewModels  5   	        MangaDownloader.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4nc%clr-namespace:MangaDownloader.Helpers     5   	   /localclr-namespace:MangaDownloader     5   	   DlocalViewModels(clr-namespace:MangaDownloader.ViewModels     5   	     � Title$  
MainWindow��5
   	   $	��350��6   $	��400��6)   
�����F� � 	Resources 5         loadingBlink g� RepeatBehavior �� 
TargetName �� TargetProperty W� From	 W� To g� Duration g� AutoReverse��%�   &	        �� 5   
   $ Forever��6*   .��5      W� $ loadingBlock��6   $ Opacity�5   &   $ 1X�5   &   $	 0.1X�5   &   $ 0:0:1D�5   &    . $
 100x��5   &   5      5      	 � DataContext	 5      4  .MangaDownloader.ViewModels.MainWindowViewModel  5   
   .��5      � $��10q�6   
 � RowDefinitions
 5   
   �� 5      $��10 �6   �� 5      $��30 �6   �� 5       $��30 �6   �� 5!      $��* �6    � ColumnDefinitions 5#   
   �� 5$      $	��140 �6   �� 5%      $��* �6   .+�5)   
   �� $��1��6   $��0��6!   $��Choose manga source:��61   �� 5*   
   -   5         ��Name$ cbMangaSources��5*      $��1��6+   $��1��68   ��5+      �� *MangaSources+ �� SelectedItem 68   �� *SelectedSource+�� 5-   
   $��2��6   $��0��6!   $��Manga name:��61   y�6D   �� *MangaNameVisibility+�� 5.   
   -   5         ��Name$ tbMangaName��5.      $��2��6�   $��1��6�   ��6'   �� *FilterMangaName+ �� UpdateSourceTrigger$ PropertyChanged=�y�6m   �� *MangaNameVisibility+6�   �� 50   
   -   5         ��Name$ loadingBlock��50      $��2��6*   $��2��67   $��0��6H   $��2��6X   $��Downloading manga list...��51      
���""��$��Center=�6L   $��24(�6i   y�52   B   �� *LoadingMangaListVisibility+53      �� 55   
   $��2��6   $��2��6    $��0��61   $��2��6A   ��56      �� *	MangaList+ 65   �� *MangaListSelectedItem+y�6d   �� *MangaListViewVisibility+ 57       �� 
TargetType �� Property �� Value��%Y   '��       �� 58      " ��6   .�59      ��  � y�$ 	Collapsed=�63   5:      5;      ��5<      �� 5=      .R�5>      ]� $��35��6    $��Stretch=�6,   
����� �.+�5?      �� $
��Left=�6$   ��6:   �� *Value+�� 5@      $��Right=�6!   $��Add to my library}��68   5A      5B      5I      5K      5L      
   M S B A M L   `   `   `  ����@  <Tools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Tools.ViewsTools.Views  5      B >AduSkin, Version=1.1.1.7, Culture=neutral, PublicKeyToken=nullP5clr-namespace:AduSkin.Controls.Metro;assembly=AduSkinAduSkin.Controls.Metro 5      Q-clr-namespace:Tools.ViewModel.ModuleViewModelTools.ViewModel.ModuleViewModel  5          Tools.Views.PdfTool   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Tools.Views     5      GMetro5clr-namespace:AduSkin.Controls.Metro;assembly=AduSkin     5      C	viewmodel-clr-namespace:Tools.ViewModel.ModuleViewModel     5        � DataContext  5      6   0Tools.ViewModel.ModuleViewModel.PdfToolViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $��* �6   .+�5   
   �� ���#FFF$��0,0,1,0q�6#      DefaultBorderBrush#��[  6=   ._�5      � .+�5      �� $��0q�6B   
������ ��6   �� *ViewSource.View+ �� SelectedItem 6o   �� *CurrentShowControl+ � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6    .�5      ��  � ��
 ���� 6;   �� 5       � ��$ 0q�6@   �� 5       � ��$ 0q�68   �� 5       �@�Cursor$
 Hand��67   6F   5      5      ��5       �� 5!      .R�5"      ��  ��Name$ Bd��6W   $��40��6%   $	��248��64   ���	#00000000���#EBEBEB$��0 1 0 0q�6y   ._�5#   "   � .+�5,   &   �� $��12(�6G     DefaultBrush#��[ 6U   $
��20,0q�6�   $��Center=�6�   $
��Left=�6�   ��60   �� *Title+-  'AduSkin.Controls.Metro.TextBlockService   HighlightText 6�   �� 	 �� ElementName$	 	SearchKey��
 �� Path$

 Text�6�   5-   #   5.       �� Triggers 5/      �� 50   "    �� Value$
 True��6�    �� Binding 6.   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" ��$
 
IsSelected�.a�51   &   ��  �� 
TargetName$ Bd��6-    � �� �#EAEAEA6S   �� 52   &   $ Bd��6-    � ��$ 0,0,5,0q�6X   �� 53   &   $ Bd��6-    � �� �#FF99006T   54   #   ]� 55   "    ]� 
Conditions 56   &   �� 57   *    �� Property �@J�IsMouseOver �� Value$
 True��6K   ./�59   &   �� $ Bd��6-    � ��
 �����6S   �� 5:   &   $ Bd��6-    � ��$ 0,0,5,0q�6X   �� 5;   &   $ Bd��6-    � �� �#46A3FF6T   5<   #   P� 5=   "    P� Property � {� P� Value$ False��6?   . �5>   &   �� $ Bd��6-   # � ��6_    � ��5?   #   5A      5C      5D      5E      � 5G   
    % !MainContentForegroundDrawingBrush#��[ 6   $��1��6Q   .+�5H      �� $��Stretch=�6   $��Stretch=�63    �� Padding$ 
40,0,40,40q�6O   $��0q�6d   $	��800��6p   $	��600��6   ���#FFF �� CornerRadius$ 6��6�     
CardShadow J� Effect# [ 6�   ._�5I      �  � RowDefinitions 5J      �� 5K      $��60 �6(   �� 5L      $	��200��6(   $
��Auto �68   .+�5N      �� $��0��6    $��12(�6E   $��Center=�6S   $
��20,0q�6n   ��6-   �� *Title+6n   �� 5O      $��1��6'   ��64   �� *	Content+5P      5R      5S      5T      5U      
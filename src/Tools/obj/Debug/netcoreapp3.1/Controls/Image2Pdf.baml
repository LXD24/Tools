   M S B A M L   `   `   `  ����B  >AduSkin, Version=1.1.1.7, Culture=neutral, PublicKeyToken=nullP5clr-namespace:AduSkin.Controls.Metro;assembly=AduSkinAduSkin.Controls.Metro  5      	 Tools   Tools.Controls.Image2Pdf   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Gmetro5clr-namespace:AduSkin.Controls.Metro;assembly=AduSkin     5        � DataContext  5
      ��    Locator �� Source# [   �� Path$ 	Image2Pdf� � 	Resources 5       ! DefaultColumnHeaderBackground �� Color " DefaultColumnHeaderBorderBrush  DefaultBorderBrush  DefaultDataGridColumnHeader �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  DefaultDataGridRow)  #System.Windows.Controls.DataGridRow   DefaultRowHeaderGripperStyle  DefaultDataGridRowHeader:  4System.Windows.Controls.Primitives.DataGridRowHeader  DefaultDataGridCell*  $System.Windows.Controls.DataGridCell �� 
TargetType	 J� SnapsToDevicePixels
 �� ContentStringFormat �� ContentStringFormat��%�  &	       &	 !     &	 B     &	 c     7  7  &	 _    &	 ,    &	 �    &	 �    7  �� 5   
   $ #FAFAFA��6@   �� 5   
   $ #EEEEEE��6A   �� 5   
   $ #EEEEEE��65   �� 5   
   "  64   .�5      ��  �@J�SnapsToDevicePixels$
 True��64   �� 5       � ��$ 5��6)   �� 5       � ��$ 25��6*   �� 5       � ��$ 40��6'   �� 5       � �� �#0006+   �� 5       � ��$ Center=�6;   �� 5        � ��$ Center=�69   �� 5#       � ��$ SemiBold"�6+   6>   �� 5$       � ��$ 14(�6)   �� 5%       � ��$ 0,0,0,3q�60   �� 5&       � ��8   6,   �� 5'       � ��8  6+   5(      �� 5+   
   "  6+   .�5,      ��  � �� �#0006+   �� 5-       �@�Cursor$
 Hand��6'   �� 5.       � �� �#EE796F6,   �� 50       � ��$
 Auto��6'   66   51      �� 54   
   " n�65   .�55      ��  � ��$ 6��6'   �� 56       � ��
 ���� 6+   �� 57       �@�Cursor$ SizeNS��6'   58      �� 5;   
   "  61   .�5<      ��  � ��$ Stretch=�6;   �� 5=       � ��$ Center=�69   �� 5>       � ��
 ���� 6,   �� 5?       � �� �#0006+   �� 5@       � ��$ 10(�6)   �� 5A       � ��$ 30��6'   �� 5B       � ��$
 Bold"�6+   5C      �� 5F   
   "  5G      .�5H      ��  � ��
 ���� 6+   �� 5I       � ��8   6,   �� 5J       � ��$ 0,0,0,1q�60   �� 5K       � ��$ Center=�6;   �� 5L       � ��$ Center=�69   �� 5M       �@J�SnapsToDevicePixels$
 True��64   �� 5N       � ��$ 0q�6(   �� 5O       � �� 5P      �� 5Q      "  6&   .R�5R      �� #��z��6!   #��z��5S      #��z��5T      #	 z	 5U      ._�5V      �� #��z��6/   #��z��5W   +   # z
 6O   #��z��5X   +   #	 z	 6N   #��z��5Y   +   #��z��5Z      5\      5]      5c      5e      5f      5g      .��5h      �  � RowDefinitions 5i   
   �� 5j      $��50 �6   5l      �� 5m      $��* �6   5o      �� 5p      $��50 �6   5r      �� 5s      $��50 �6   5u      .+�5x   
   �� $��Right=�6    �� Orientation$ 
Horizontal=�61   $��0��6J   .+�5y      *   $AduSkin.Controls.Metro.AduFlatButton  $	��100��6q   $��0 0 10 0q�6}   $��添加图片��6�      Type$
 info=�6�   ��6"   �� *SelectImagesCommand+{�6K   �� $ 
BtnEnabled�6�     5z      $	��100��6p   $��清空列表��6|   $ warning=�6�   ��6"   �� *ClearImagesCommand+{�6J   �� $ 
BtnEnabled�6�   5{      � 5}   
   $��1��6   $	��400��6   .+�5~      �� -   5         ��Name$ ImageContainer��5~      #��[ 6-   $��1q�6`   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dg��5      ��6'   �� *Images+��5�      �� 5�      "  6    .�5�      ��  � ��$	 400��6:   �� 5�       �@ MinRowHeight$ 60Q�6=   �� 5�       � ��
 �����6;   �� 5�       � ��
 ���� 6<   �� 5�       � ��$ 0q�6@   �� 5�       �@ ColumnHeaderStyle# [ 6B   �� 5�       �@ RowHeaderStyle# [ 6?   �� 5�       �@ 	CellStyle# [ 6:   �� 5�       �@ RowStyle# [ 69   �� 5�       �@ HeadersVisibility$	 All=�6B   �� 5�       �@ EnableRowVirtualization$
 True��6H   �� 5�      " �@ EnableColumnVirtualization$ False��6K   �� 5�       �@ AutoGenerateColumns$ False��6D   �� 5�       �@ 
IsReadOnly$
 True��6;   �� 5�       �@ SelectionMode$ Single=�6>   �� 5�       �@ SelectionUnit$ FullRow=�6>   �� 5�       �@ GridLinesVisibility$
 None=�6D   �� 5�       �@��AlternationCount$ 2��6A   6M   �� 5�       � ��$
 True��6N   �� 5�       �@%�IsVirtualizing$
 True��6V   6e   �� 5�       �@%�VirtualizationMode$ 	Recycling=�6Z   �� 5�      " �@��IsDeferredScrollingEnabled$ False��6X   �� 5�       �@J�	AllowDrop$
 True��6:   5�         Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ 文件名称��61      Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 6?      CellTemplate 5�      �� 5�   "   .R�5�   &   �� ��6,   �� *Name+5�   #   5�        5�      $ 文件路径��61   $ *	 6?    5�      �� 5�   "   .R�5�   &   �� ��6,   �� *Path+5�   #   5�        5�      $ 操作��61   $	 200	 6=    5�      �� 5�   "   .R�5�   &     -   5        $��50��5�   t   $��移除��6   $ warning=�6�   ��6R   �� *Path+6�   5�   #   5�      5�      5�      5�      �� 5�   
   $��Right=�6   $ 
Horizontal=�61   $��2��6J   .+�5�      �� $>
 8* 拖动行可以调整顺序,共选择了 {0} 张图片��6   $��12(�6C   ��6R   �� *Images.Count+5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Stretch=�6.   $��Right=�6J   $��3��6f   .+�5�        $	��200��6k   $
 info=�6�   {�6"   �� $ 
BtnEnabled���6H   �� *ExecuteCommand+��6{   �� *
BtnContent+6�   5�      � 5�   
   -   5         �Name$ Popup1��5�       J� IsHitTestVisible$ False��6    $��RelativePoint=�69    � AllowsTransparency$
 True��6T   .��5�      �� 
����İ�$��2q�63   
������� J� Opacity$
 0.95Q�6Z   ._�5�      �� $ 
Horizontal=�6   $��4,3,8,3q�67   .+�5�      �� $��14(�6D   $
��Bold"�6S   $��Center=�6e   $��8,0,0,0q�5�   "   ��5�       �� *SelectedItem.Path+5�   "   5�      5�      5�      5�      5�      
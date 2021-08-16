# Xam.Views.NepaliDatePicker
Nepali Date Picker for Xamarin Forms

**Installation**   

```  Install-Package Xam.Plugins.NepaliDatePicker -Version 1.0.0 ```

**Usage**   

1. Import namespace in your xaml file   

```  xmlns:CustomControls="clr-namespace:Xam.Plugins.NepaliDatePicker.CustomControls;assembly=Xam.Plugins.NepaliDatePicker"  ```

2. Use CustomControl wherever required   

```    <CustomControls:NepaliDatePickerEntry DateFormat="mDy" Separator="/" CurrentDate="04/31/2078" /> ```   

3. Since, it uses [Rg.Plugins.Popup](https://github.com/rotorgames/Rg.Plugins.Popup/wiki/Getting-started) for showing calendar , initialise it in platform specific projects.

**Available Bindable Properties**   
This plugin is a customisation on existing Entry . So, you can use all existing bindable properties of entry. Below are the bindable properties provided by this plugin

 ***CurrentDate***   
 Provide initial Value to the control by setting value in this property .Value defaults to String.Empty
 
 ***Separator***   
 Takes char value and used to separate the day,month and year parts . Value defaults to "-"
 
 ***DateFormat***   
 gets/sets Text value in control in specified format . Available Options are 
 1. yMd
 2. dMy
 3. mDy   
 
 Value defaults to yMd
 

**Working Screenshot**  
![alt text][screenshot]

[screenshot]: https://github.com/solo-developer/Xam.Views.NepaliDatePicker/blob/main/GIF-210816_112709.gif "Xamarin Nepali DatePicker"

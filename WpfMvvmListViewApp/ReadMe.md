## Community.Mvvm Example 8.1.0

- **Project TargetFramework .Net 7.0**
- **Wpf Binding**
- **UserControl**
- **Behavior Command**

-  **MultiBinding**
```CSharp
<Button
    x:Name="ClearBtn"
    Width="75"
    Height="25"
    Margin="10,5"
    VerticalAlignment="Center"
    Command="{Binding ClearCommand}"
    Content="Clear">
    <Button.CommandParameter>
        <MultiBinding Converter="{StaticResource TextBoxNamesConverter}">
            <Binding ElementName="idSearchText" />
            <Binding ElementName="searchText" />
        </MultiBinding>
    </Button.CommandParameter>
</Button>
```

- **IMultiValue Converter**
```CSharp
public class TextBoxNamesConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        #region Tuple로 TextBox 컨트롤을 넘기는 방법
        TextBox textBox1 = (TextBox)values[0];
        TextBox textBox2 = (TextBox)values[1];
        return new Tuple<TextBox, TextBox>(textBox1, textBox2);
        #endregion

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

- **Dependency Injection**
```CSharp
public partial class App : Application
    {
        public App() => Services = ConfigureServices();
        public new static App Current => (App)Application.Current;

        public IServiceProvider? Services { get; }
        private static IServiceProvider? ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
```
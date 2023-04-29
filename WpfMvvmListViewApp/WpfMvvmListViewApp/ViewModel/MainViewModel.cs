using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CustomerLib;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace WpfMvvmListViewApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ICustomerRepository? _customerRepository;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        private Customer? _selectedCustomer;

        public MainViewModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));

            ListViewCollectionViewSource = new CollectionViewSource
            {
                //public IEnumerable<Customer>? Customers => _customerRepository?.Customers; //Customers

                Source = _customerRepository.Customers //Customers
            };

            ListViewCollectionViewSource.Filter += ApplyFilter;
        }

        #region 검색어 입력시 자동으로 필터링
        // 자동 검색 {Binding ListViewCollection} 변경해야 작동
        private CollectionViewSource ListViewCollectionViewSource { get; set; }
        public ICollectionView ListViewCollection
        {
            get => ListViewCollectionViewSource.View;
        }
        private void OnFilterChanged()
        {
            // Refresh하게되면 CollectionViewSource의 Source로 지정된 Customer가 차례로 ApplyFilter의 FilterEventArgs로 들어가게 됩니다.
            ListViewCollectionViewSource.View.Refresh();
        }
        private string? _filter;
        public string? Filter
        {
            get => _filter;
            set
            {
                SetProperty(ref _filter, value);
                // filter text가 변경되면 검색 조건이 변한 것이므로 필터를 refresh하여 데이터를 다시 검색하도록 합니다.
                OnFilterChanged();
            }
        }
        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            var filterCustomer = (Customer)e.Item;

            // Filter Text 가 비어있다면 필터를 적용하지 않고 모든 콘텐츠를 보여주어야 하기때문에 true를 대입합니다.
            if (string.IsNullOrWhiteSpace(this.Filter) || this.Filter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                // 대소문자 구분없이 찾기 위해 모두 소문자로 변경하여 검색하도록 합니다.
                // 또한 완전 일치가 아닌 함유하고 있어도 찾기 위하여 Equals가 아닌 Contains를 사용하였습니다.
                e.Accepted = filterCustomer.CustomerId.ToLower().Contains(Filter.ToLower());
            }
        }
        #endregion

        //public IEnumerable<Customer>? Customers => _customerRepository?.Customers;

        [RelayCommand]
        private void Add()
        {
            var customer = new Customer();
            _customerRepository?.Add(customer);
            SelectedCustomer = customer;
            OnPropertyChanged(nameof(Customer));
        }
        private bool HasSelectedCustomer() => SelectedCustomer != null;

        [RelayCommand(CanExecute = "HasSelectedCustomer")]
        private void Remove()
        {
            if (SelectedCustomer != null)
            {
                _customerRepository?.Remove(SelectedCustomer);
                SelectedCustomer = null;
                OnPropertyChanged(nameof(Customer));
            }
        }

        [RelayCommand]
        private void Save()
        {
            _customerRepository?.Commit();
        }

        [RelayCommand]
        private void Search(string? textToSearch)
        {
            //var coll = CollectionViewSource.GetDefaultView(ListViewCollection);
            //if (!string.IsNullOrWhiteSpace(textToSearch))
            //    coll.Filter = c => ((Customer)c).Country.ToLower().Contains(textToSearch.ToLower());
            //else
            //    coll.Filter = null;
            var coll = CollectionViewSource.GetDefaultView(ListViewCollection);
            if (!string.IsNullOrWhiteSpace(textToSearch))
            {
                coll.Filter = c => ((Customer)c).Country.ToLower().Contains(textToSearch.ToLower());
            }
            else
            {
                coll.Filter = null;
            }
        }

        // Country 검색 Text를 지우기 위해 만든 TextBox.Text Property
        [ObservableProperty]
        private string? _textProperty;

        // IMultiValueConverter 컨버터를 이용해서 2개의 TextBox.Text Clear
        [RelayCommand]
        private void Clear(object? parameter)
        {
            //TextProperty = string.Empty;
            //var collectionView = CollectionViewSource.GetDefaultView(ListViewCollection); // Customers
            //collectionView.Filter = null;
            //ListViewCollectionViewSource.Filter += ApplyFilter;

            // Clear 버튼이 클릭될 때 실행되는 코드
            // 파라미터로 전달된 Tuple을 사용하여 TextBox의 값을 비웁니다.
            //Tuple<string, string>? tuple = parameter as Tuple<string, string>;

            // IMultiValueConverter Tuple로 값 반환
            Tuple<TextBox, TextBox>? tuple = parameter as Tuple<TextBox, TextBox>;

            tuple.Item1.Text = string.Empty;
            tuple.Item2.Text = string.Empty;

            //var tuple = parameter as List<TextBox>; 
            //foreach (var item in tuple)
            //{
            //    item.Text = string.Empty;
            //}

            var collectionView = CollectionViewSource.GetDefaultView(ListViewCollection); // Customers
            collectionView.Filter = null;
            ListViewCollectionViewSource.Filter += ApplyFilter;
        }
    }
}

using System.ComponentModel;

namespace SmartOffice.Models.OrderModels;

public class OrderDetailsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }

    private string _orderdetailsUsername = "";
    private string _orderdetailsDishDesignation = "";
    private string _orderdetailsUserPaymentMethod = "";
    private string _orderdetailsDishPrice = "";
    
    public string OrderdetailsUsernameProp
    {
        get => _orderdetailsUsername;
        set
        {
            _orderdetailsUsername = value;
            OnPropertyChanged(nameof(OrderdetailsUsernameProp));
        }
    }
    
    public string OrderdetailsDishDesignationProp
    {
        get => _orderdetailsDishDesignation;
        set
        {
            _orderdetailsDishDesignation = value;
            OnPropertyChanged(nameof(OrderdetailsDishDesignationProp));
        }
    }
    
    public string OrderdetailsUserPaymentMethodProp
    {
        get => _orderdetailsUserPaymentMethod;
        set
        {
            _orderdetailsUserPaymentMethod = value;
            OnPropertyChanged(nameof(OrderdetailsUserPaymentMethodProp));
        }
    }
    
    public string OrderdetailsDishPriceProp
    {
        get => _orderdetailsDishPrice;
        set
        {
            _orderdetailsDishPrice = value;
            OnPropertyChanged(nameof(OrderdetailsDishPriceProp));
        }
    }
}
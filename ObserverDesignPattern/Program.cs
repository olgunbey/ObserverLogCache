// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
IObserverTransaction transaction = new ObserverIslem();
UrunIslemler urunIslemler = new UrunIslemler(transaction); //gelince bak
Urun urun = new Urun()
{
    Name = "ürün1",
    ID = 1,
    Money = 120,
    Stock = 100
};

transaction.Attach(new Logger());
urunIslemler.UrunSat(urun);

public class Urun
{
    public int ID { get; set; }
    public decimal Money { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
}

interface IObserverTransaction
{
    List<IObserver> Observers { get; set; }
    void Attach(IObserver observer);
    void Delete(IObserver observer);
}

interface IUrunIslem
{
    void UrunSat(Urun urun);
}

class ObserverIslem:IObserverTransaction
{
    public ObserverIslem()
    {
        Observers = new List<IObserver>();
    }
    public List<IObserver> Observers { get; set; }

    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void Delete(IObserver observer)
    {
        Observers.Remove(observer);
    }
}
class UrunIslemler:IUrunIslem
{
    private readonly IObserverTransaction _observerTransaction;
    public UrunIslemler(IObserverTransaction observerTransaction)
    {
        _observerTransaction = observerTransaction;
    }


    public void UrunSat(Urun urun)
    {
        urun.Stock -= 1;
        Notify(urun);
    }

    private void Notify(Urun urun)
    {
        foreach (var VARIABLE in _observerTransaction.Observers)
        {
            VARIABLE.Logla(urun);
        }
    }
    
}

interface IObserver
{
    void Logla(Urun urun);
}



class Logger:IObserver
{

    public void Logla(Urun urun)
    {
        Console.WriteLine(urun.Name +" "+ urun.Stock+" dosyaya loglandı");
    } 
}

class CacheLogger : IObserver
{

    public void Logla(Urun urun)
    {
        Console.WriteLine(urun.Name +" "+ urun.Stock+" cache loglandı");
    }
}
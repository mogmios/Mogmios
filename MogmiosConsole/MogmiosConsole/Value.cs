using System;

namespace Mogmios {
 internal enum SortOrder : int {
  LessThan = -1,
  SameAs = 0,
  GreaterThan = 1
 }

 public class ImmutableValue : IValue {
  public ImmutableValue () : base () { }

  public ImmutableValue ( object value ) : this () {
   _Value = value;
   _HasValue = true;
  }

  protected bool _HasValue = false;
  public bool HasValue
  {
   get
   {
    return _HasValue;
   }
  }

  protected object _Value = null;

  public object Value
  {
   get
   {
    return _Value;
   }
  }

  public object GetValueOrDefault () {
   return ( HasValue ? Value : null );
  }

  public bool Equals ( IValue other ) {
   if ( other == null ) {
    return false;
   }
   return ( Value == other.Value );
  }

  public int CompareTo ( IValue other ) {
   var value = Value;
   var otherValue = other.Value;

   if ( value == otherValue ) {
    return (int) SortOrder.SameAs;
   }

   if ( otherValue == null ) {
    return (int) SortOrder.GreaterThan;
   }

   return (int) SortOrder.LessThan;
  }
 }

 public class ImmutableValue<T> : ImmutableValue, IValue<T> {
  public ImmutableValue ( T value ) : base ( value ) {
  }

  public static implicit operator ImmutableValue<T>( T value ) {
   return ( new ImmutableValue<T> ( value ) );
  }

  public static implicit operator T ( ImmutableValue<T> value ) {
   return value.Value;
  }

  public new T Value
  {
   get
   {
    return (T) base.Value;
   }
  }

  public new T GetValueOrDefault () {
   return ( HasValue ? Value : default ( T ) );
  }

  public T GetValueOrDefault ( T defaultValue ) {
   return ( HasValue ? Value : defaultValue );
  }

  public bool Equals ( IValue<T> other ) {
   if ( other == null ) {
    return false;
   }
   if ( HasValue != other.HasValue ) {
    return false;
   }
   if ( Value == null ) {
    return ( other.Value == null );
   }
   return ( Value.Equals ( other.Value ) );
  }

  public int CompareTo ( IValue<T> other ) {
   if ( ( Value is IComparable<T> ) && ( other.Value is IComparable<T> ) ) {
    var myComparable = Value as IComparable<T>;
    return myComparable.CompareTo ( other.Value );
   }

   if ( Equals ( other ) ) {
    return (int) SortOrder.SameAs;
   }

   if ( other == null ) {
    return (int) SortOrder.GreaterThan;
   }

   if ( HasValue != other.HasValue ) {
    if ( HasValue ) {
     return (int) SortOrder.GreaterThan;
    }
    return (int) SortOrder.LessThan;
   }

   if ( Value == null ) {
    if ( other.Value == null ) {
     return (int) SortOrder.SameAs;
    }
    return (int) SortOrder.LessThan;
   } else if ( other.Value == null ) {
    return (int) SortOrder.GreaterThan;
   }

   if ( Value.Equals ( other.Value ) ) {
    return (int) SortOrder.SameAs;
   }

   return (int) SortOrder.SameAs;
  }
 }

 public class MutableValue : ImmutableValue, IMutableValue {
  public MutableValue ( object value ) : base ( value ) { }

  public new object Value
  {
   get
   {
    return _Value;
   }

   set
   {
    _Value = value;
   }
  }
 }

 public class MutableValue<T> : ImmutableValue<T>, IMutableValue<T> {
  public MutableValue ( T value ) : base ( value ) {
  }

  object IMutableValue.Value
  {
   get
   {
    return Value;
   }

   set
   {
    Value = (T) value;
   }
  }

  public new T Value
  {
   get
   {
    return (T) _Value;
   }

   set
   {
    _Value = value;
   }
  }
 }
}

// static Boolean Parse ( string value );
// static Boolean TryParse ( string value, out INumber result ) {
// return false;
//}

//public static implicit operator ImmutableValue<T>( T value ) {
// return ( new ImmutableValue<T> ( value ) );
//}

//public static implicit operator T ( ImmutableValue<T> value ) {
// return value.Value;
//}

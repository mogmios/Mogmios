using System;

namespace Mogmios {
 public interface IValue : IComparable<IValue>, IEquatable<IValue> {
  bool HasValue { get; }
  object Value { get; }
  object GetValueOrDefault ();
 }

 public interface IValue<T> : IValue, IComparable<IValue<T>>, IEquatable<IValue<T>> {
  new T Value { get; }
  new T GetValueOrDefault ();
  T GetValueOrDefault ( T defaultValue );
 }

 public interface IMutableValue : IValue {
  new object Value { get; set; }
 }

 public interface IMutableValue<T> : IMutableValue, IValue<T> {
  new T Value { get; set; }
 }

 public interface INumber : IValue, IConvertible, IComparable<INumber>, IEquatable<INumber> {
  new INumber Value { get; }
  INumber ToNumber ();
  bool ToBool ();
  sbyte ToSByte ();
  byte ToByte ();
  char ToChar ();
  short ToShort ();
  ushort ToUShort ();
  int ToInt ();
  uint ToUInt ();
  long ToLong ();
  ulong ToULong ();
  float ToFloat ();
  double ToDouble ();
  decimal ToDecimal ();
 }

 public interface INumber<T> : INumber, IValue<T>, IComparable<INumber<T>>, IEquatable<INumber<T>> {
  new INumber<T> Value { get; }
 }
}

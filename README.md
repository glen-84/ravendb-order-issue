# README

With `ravendb/ravendb:5.4-ubuntu-latest`:

```
Foo.BarBool: True, Foo.BarShort: 14
Foo.BarBool: True, Foo.BarShort: 12
Foo.BarBool: False, Foo.BarShort: 13
Foo.BarBool: , Foo.BarShort:
```

With `ravendb/ravendb:6.0-ubuntu-latest`:

```
Foo.BarBool: False, Foo.BarShort: 13   # BarBool should be ordered descending, so `True` first.
Foo.BarBool: True, Foo.BarShort: 14
Foo.BarBool: True, Foo.BarShort: 12
# Missing 4th record.
```

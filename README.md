# README

With `ravendb/ravendb:5.4-ubuntu-latest`:

```
Foo.BarBool: True, Foo.BarShort: 14
Foo.BarBool: True, Foo.BarShort: 12
Foo.BarBool: False, Foo.BarShort: 13
```

With `ravendb/ravendb:6.0-ubuntu-latest`:

```
Foo.BarBool: True, Foo.BarShort: 12
Foo.BarBool: True, Foo.BarShort: 14     <-- Not descending
Foo.BarBool: False, Foo.BarShort: 13
```

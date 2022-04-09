# Gui Basics

In Onlooker, interfaces are stored and processed in Xml. Similar to Xaml, everything is structured hierarchically.

For example,

```xml
<frontend_root>
    <label text="Hello world!" x_pos="50%" y_pos="50%"/>
</frontend_root>
```

Assuming the context is of the window itself, "Hello world!" should be displayed in the center of the screen. Simple, right?

To clarify, a [Label](label.md) is a primitive element, that is, builtin. It serves as a way to display static text, non-interactive text. [Buttons](button.md), [TextBoxes](button.md), and [FormattedLabel](formatted_label.md) are examples which deviate from [Label](label.md).

There's other Gui elements for different purposes as well.

```xml
<vertical_layout>
    <label text="Top"/>
    <label text="Middle"/>
    <label text="Bottom"/>
</vertical_layout>
```

A layout repositions and resizes its children every frame. The vertical layout stacks each of its children in descending order on the same X axis. The resulting display here should look like such,

```txt
---------
|  Top  |
------------
|  Middle  |
------------
|  Bottom  |
------------
```

Do note that labels only stretch like this when the `rect_scales` attribute is set to true.

Of course, there's also HorizontalLayout, as well as GridLayout among others.

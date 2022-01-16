# Race Config Documentation

## General Values

<br/>

#### Reproduction Method <br/>
Literal Name: `reproduction_method` <br/>
Required: False, defaults to None
Type: Enum <br/>
> None: Does not reproduce <br/>
> Heterosexual: Requires another of the same race and another gender to reproduce <br/>
> Homosexual: Requires another of the same race and same gender to reproduce <br/>
> Asexual: Reproduces on their own <br/>

#### Maximum Knowledge <br/>
Literal Name: `maximum_knowledge` <br/>
Required: False, defaults to 0
Type: Unsigned long (from 0 to 18,446,744,073,709,551,615)

Determines the cutoff point where previous knowledge is warped, corrupted, or removed

###TODO
#Quickstart for implementing SpecExpress
# Quickstart #
_Adding SpecExpress to your project_

There are multiple methods of defining validation rules. Below is the recommended method for most developers, but DynamicallyAddingSpecifications outside this base classe is also supported.

## Initialize the Container ##
First, somewhere in your application startup (Global.asax for example), you must initialize the Validation container. This will search your application for any Specifications that you've created and registered them for use later.

```
ValidationContainer.Scan(x => x.TheCallingAssembly());

Assembly assembly = Assembly.LoadFrom("SpecExpress.Test.Domain.dll");
ValidationContainer.Scan(x => x.AddAssembly(assembly));

ValidationContainer.Scan(x => x.AddAssembliesFromPath(@"c:\myproject"));

```

## Create a Specification ##
Create a new class, inherit from SpecificationBase

&lt;T&gt;

.

The first step in the Validation DSL is to establish if the error level. Check is an error and Warn is a warning. For example, a rule that would generate an error would be:

Required and Optional

```
public class ContactSpecification : SpecificationBase<Contact>
    {
        public ContactSpecification()
        {
            Check(c => c.FirstName).Required();
            Check(c => c.MiddleName).Optional();
            Check(c => c.LastName).Required();
        }
    }
```
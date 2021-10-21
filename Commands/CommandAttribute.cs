[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)  
]  
public class CommandAttribute : System.Attribute  
{  
    public string Mame;
  
    public CommandAttribute(string name)  
    {  
        Name = name;
    }  
}

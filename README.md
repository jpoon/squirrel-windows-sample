# .NET + Squirrel

This project serves as a demonstration of using [Squirrel](https://github.com/Squirrel/Squirrel.Windows) as an update framework for a .NET desktop application. 
Although this particular project is a console application, the same pattern can be generalized to apply to other .NET project types -- WPF, ASP.NET, etc.

## Getting Started

The project is a self-hosted web server.
When built and run, it will service the following endpoint `GET http://localhost:8080/api/test` and return a `200` HTTP status and `hello` message.

The following shows how to build a Squirrel package in which you would distribute:

1. Build the solution
2. Create a nuget package according to the nuspec: `nuget pack .\HelloWorld.nuspec -version <your-version-here>`
3. Based on the nuget package, create the Squirrel packages: `Squirrel --releasify HelloWorld.<your-version-here>`

where <your-version-here> is in the form w.x.y.z

## Resources

* `/Docs` - This project was initially built to supplement a talk that I had done at Decoded Open Source Conference (#DecodedConf) in Dublin, Ireland 2016. 
Under this folder you can find some of my presentation material.
* [Blog Post](http://www.jasonpoon.ca/2016/02/20/setting-up-continuous-delivery-with-squirrel-and-vsts/) - Blog post I had written about setting up a continuous delivery pipeline using Squirrel and VSTS.
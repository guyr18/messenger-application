## Messenger Application
Messenger Application is a chat messaging application that was developed as an educational resource for learning the fundamentals behind C# and Microsoft SQL
Server 2019. Additionally, it gave me experience with common T-SQL functionalities such as composite indexing, data retrieval, and data insertion(s).

This application differs from the standard chat messenger in that it does not utilize a TCP/IP networking approach. Instead, it opts to make heavy
use of multi-threading and locking mechanisms to consistently take "snapshots" of the database at a particular time. When changes are detected, the
main UI thread is signaled, and effectively updated based on the newly retrieved information. Due to the nature of query repetition, non-clustered
indexing yielded a great performance benefit.

There are pros and cons to this approach, however it was deemed appropriate for quickly introducing myself to the backend components previously mentioned.

## Demonstration Video
This application was developed by targeting a Windows 10 machine utilizing CLR and the .NET Core (3.1). The following is a set of sample videos showcasing how you may run this project and the intended use / functionality of the application.

**User Authentication and Messaging Functionality**: https://vimeo.com/725575629  
**Conversation Updates**: https://vimeo.com/725581436  
**Registration System**: https://vimeo.com/725584303  


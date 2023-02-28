
<h1 align="center"> ShoppingList</h1>
<h2 align="center"> Description</h2>
<h3 align="center"> A shopping list has been created in this REST API. 
 Multiple categories can be added to the shopping list and the items within the categories can be changed. 
 User and Admin roles have been created using tokens in the application.</h1>
  
```diff 
- How to start up

+ With using Visual Studio start button is enough

! With using Visual Studio Code
```
<code>dotnet watch run</code> 
```diff 
-After start up

+To get access to Request "User" or "Admin" must be created by
```
  <code>POST</code><code>/Users</code><br>
  <code>POST</code><code>/Users</code><code>/Connect</code><code>/Token</code>
```diff
+To send HTTP request you should use accessToken given

- Category HTTP REQUEST's
```
<code>GET</code><code>/api</code><code>/Category</code><code>/GetAll</code><br>
<code>GET</code><code>/api</code><code>/Category</code><code>/{GetById}</code><br>
<code>GET</code><code>/api</code><code>/Category</code><code>/CreateDate</code><br>
<code>GET</code><code>/api</code><code>/Category</code><code>/FinishDate</code><br>
<code>POST</code><code>/api</code><code>/Category</code><code>/CreateCategory</code><br>
<code>PUT</code><code>/api</code><code>/Category</code><code>/{UpdateCategory}</code><br>
<code>DELETE</code><code>/api</code><code>/Category</code><code>/{DeleteCategory}</code><br>
```diff 
- List HTTP REQUEST's
```

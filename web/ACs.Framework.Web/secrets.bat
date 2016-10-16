echo off
call user-secret set "Smtp:Server" "your-server.com.br" 
call user-secret set "Smtp:From" "email@domain.com.br"
call user-secret set "Smtp:UserName" "username"
call user-secret set "Smtp:Password" "password"
echo on
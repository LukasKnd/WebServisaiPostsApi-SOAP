#Running
```
git clone https://github.com/LukasKnd/WebServisaiPostsApi.git
cd WebServisaiPostsApi
docker build -t posts-api Api
docker run -p 80:80 posts-api
```

Open swagger at
````
localhost/swagger
````
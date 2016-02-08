FROM nice/alpine-fsharp:2f00052c29ce34a5ce8e765b287b6e5072c1b22e	

MAINTAINER James Kirk <james.kirk84@gmail.com>

ADD . /ld-resource-api

WORKDIR /ld-resource-api

RUN ./build.sh && \
    ls -a . | grep -v "src" | grep -v "packages" | xargs -i rm -rf {}

CMD cd /ld-resource-api && fsharpi src/RunServer.fsx

EXPOSE 8083

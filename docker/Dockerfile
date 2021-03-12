FROM debian

RUN apt update && apt upgrade -y
RUN apt install -y gcc
RUN mkdir /app

WORKDIR /app
COPY main.c .
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

ENTRYPOINT [ "./entrypoint.sh" ]
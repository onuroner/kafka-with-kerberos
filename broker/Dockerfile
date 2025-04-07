FROM openjdk:17.0.1-jdk-slim

RUN groupadd -r kafka && useradd -r -g kafka kafka

ENV KAFKA_OPTS="--enable-native-access=ALL-UNNAMED -Djava.security.auth.login.config=/etc/kafka/kafka_server_jaas.conf -Djava.security.krb5.conf=/etc/krb5.conf"
ENV KAFKA_HEAP_OPTS="-Xms10G -Xmx10G"

RUN apt-get update && \
    apt-get install -y \
    wget \
    vim \
    telnet \
    krb5-user \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* && \
    mkdir /var/run/sshd

WORKDIR /opt

RUN wget https://downloads.apache.org/kafka/3.8.0/kafka_2.13-3.8.0.tgz && \
    tar -xzf kafka_2.13-3.8.0.tgz && \
    mv kafka_2.13-3.8.0 kafka && \
    rm kafka_2.13-3.8.0.tgz

COPY ./starter-broker.sh /opt/kafka/bin/starter-broker.sh

RUN chmod +x /opt/kafka/bin/starter-broker.sh && \
    chown -R kafka:kafka /opt/kafka && \
    #chown -R kafka:kafka /kafka
    
USER kafka

CMD ["/opt/kafka/bin/starter-broker.sh"]
#!/bin/bash

# exec /etc/kafka/docker/run &
# KAFKA_PID=$!
echo "Kafka başlatılıyor..."
# Burada gerekli bir bekleme süresi ekleyebilirsiniz
sleep 15

# Topic oluşturma
cd /opt/kafka/bin
./kafka-topics.sh --create --topic kafka_demo_topic --bootstrap-server broker:9092 --command-config /opt/kafka/config/producer.properties
./kafka-acls.sh --bootstrap-server broker:9092 --command-config /opt/kafka/config/producer.properties --add --allow-principal User:producer-client --operation All --topic kafka_demo_topic

echo "Topic: kafka_demo_topic oluşturuldu."
echo "ACL: producer-client eklendi."
tail -f /dev/null

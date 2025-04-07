FROM openjdk:17.0.1-jdk-slim

WORKDIR /mnt 
RUN chmod 777 -R /mnt

# Install common dependencies
RUN apt-get update -y && \
    DEBIAN_FRONTEND=noninteractive apt-get install -y \
    krb5-user \
    python3 \
    libsasl2-modules-gssapi-mit \
    openssl \
    libsasl2-dev \
    libssl-dev \
    python3-pip \
    git \
    wget \
    apt-transport-https \
    software-properties-common \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Verify Python installation
RUN python3 --version && pip3 --version

# Install Python requirements
COPY config/python_requirements.txt /mnt/python_requirements.txt
RUN pip3 install --no-cache-dir -r /mnt/python_requirements.txt

# Install .NET SDK
RUN apt-get update && \
    apt-get install -y wget && \
    wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-8.0 && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Verify .NET installation
RUN dotnet --version

# Copy configuration files
COPY config/krb5.conf /etc/krb5.conf
COPY init-sh/starter-client.sh /mnt/starter-client.sh
RUN chmod +x /mnt/starter-client.sh

RUN chmod 777 -R /mnt

CMD [ "bash", "/mnt/starter-client.sh" ]

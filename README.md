# Ping Utility

A simple yet powerful utility for pinging network hosts with configurable parameters.

## Features

- Ping a specified IP address or hostname
- Configurable timeout and packet count
- Detailed statistics including packet loss and average round-trip time
- Error handling for network issues
- Clear output formatting similar to standard ping utilities

## Usage

```csharp
var pingUtility = new PingUtility();
pingUtility.PingHost("google.com", 1000, 4);

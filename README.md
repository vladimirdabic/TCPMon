# TCPMon
**TCPMon** is a program that allows you to send and receive data using TCP connections.\
It's useful for debugging a protocol that you might be implementing.

## Schema
Since working with bytes directly can be difficult at times, you can use Schema to describe a schema for the data you're sending and receiving.

Each data monitor instance can have a schema loaded, Blaze can also work with schemas.\
Below is an example of a schema.

```cpp
enum packet_type :: uint8 {
    0: CHANNEL_LIST;
    1: MESSAGE;
}

struct channel {
    name :: string;
}

@entry
struct packet {
    type :: packet_type;
    
    if(type == packet_type.CHANNEL_LIST) {
        count :: uint8;
        channels :: channel[count];
    } else if(type == packet_type.MESSAGE) {
        message :: string;
    }
}
```

## Blaze
Scripting is done using the [Blaze](https://github.com/vladimirdabic/Blaze) programming language.

Each TCPMon client instance has an internal blaze module which contains the following public variables:

| Variable | Description | 
|----------|-------------|
|connection|The client connection instance|

TCPMon also has a main internal module which provides multiple libraries:

| Variable | Description | 
|----------|-------------|
| console  |Allows interaction with the console|
| parse    |Convert values into different types|
| schema   |Loading schemas for decoding data|
| module   |Load blaze modules|
| packet   |Library for handling packets|

The module hierarchy in TCPMon goes as follows:
- Internal Module
    - Connection Module
        - User module
    - Connection Module
        - User module

All of the variables described above can be accessed from your script by using `extern var ...;`

Below is an example of a Blaze script (C# highligting)
```cs
extern var console;
extern var connection;
extern var parse;
extern var packet;
extern var schema;

var packet_schema = schema.load("packet.schema");

func main() {
    console.print("Hello World");
}

event connection.received(data) {
    var decoded = data.decode(packet_schema);
    
    // print out the decoded object
    for(var pair : decoded)
        console.print(pair[0] + ": " + parse.str(pair[1]));
        
    // send a response
    var resp = packet.builder()
        .string("Hello World");

    connection.send(resp);
}

event connection.closed() {
    console.print("Bye");
}
```
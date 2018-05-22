# CoffeeChain

## 1. Install Ethereum Tool on Ubuntu 16.04 LTS
```
sudo apt-get install software-properties-common
sudo add-apt-repository -y ppa:ethereum/ethereum
sudo apt-get update
sudo apt-get install ethereum
```

Install Geth on Raspi

Download Binary :
```
curl --header 'Host: gethstore.blob.core.windows.net' --user-agent 'Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:59.0) Gecko/20100101 Firefox/59.0' --header 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' --header 'Accept-Language: en-US,en;q=0.5' --referer 'https://geth.ethereum.org/downloads/' --header 'Upgrade-Insecure-Requests: 1' 'https://gethstore.blob.core.windows.net/builds/geth-linux-arm7-1.8.6-12683fec.tar.gz' --output 'geth-linux-arm7-1.8.6-12683fec.tar.gz'
```

Unpack:
```
tar -xvzf geth-linux-arm7-1.8.6-12683fec.tar.gz
```

Move to the new directory and move the geth binary to `/usr/local/bin/`:
```
cd geth-linux-arm7-1.8.6-12683fec/
sudo mv geth /usr/local/bin/
```

## 2.1 Create Genesis File for POW or
```
{
    "config": {  
        "chainId": 987, 
        "homesteadBlock": 0,
        "eip155Block": 0,
        "eip158Block": 0
    },
    "difficulty": "0x400",
    "gasLimit": "0x8000000",  //set this really high for testing
    "alloc": {}
}
```

## 2.2 Create Genesis File for POA

### First Create on each Validation Node an Account:
```
devsrv1$ geth --datadir node1/ account new
Your new account is locked with a password. Please give a password. Do not forget this password.
Passphrase: pwdnode1 (for example)
Repeat passphrase: pwdnode1
Address: {0x87366ef81db496edd0ea2055ca605e8686eec1e6}

devsrv2$ geth --datadir node2/ account new
Your new account is locked with a password. Please give a password. Do not forget this password.
Passphrase: pwdnode2 (for example)
Repeat passphrase: pwdnode2
Address: {08a58f09194e403d02a1928a7bf78646cfc260b0}

Store Password in File to Unlock Account:
devsrv1$ echo 'pwdnode1' > node1/password.txt
devsrv2$ echo 'pwdnode2' > node2/password.txt
```

### And than Create Genesis:
```
devnet$ puppeth 
Please specify a network name to administer (no spaces, please)
> devnet
What would you like to do? (default = stats)
 1. Show network stats
 2. Configure new genesis
 3. Track new remote server
 4. Deploy network components
> 2

Which consensus engine to use? (default = clique)
 1. Ethash - proof-of-work
 2. Clique - proof-of-authority
> 2

How many seconds should blocks take? (default = 15)
> 5 // for example

Which accounts are allowed to seal? (mandatory at least one)
> 0x87366ef81db496edd0ea2055ca605e8686eec1e6 //copy paste from account.txt :)
> 0x08a58f09194e403d02a1928a7bf78646cfc260b0

Which accounts should be pre-funded? (advisable at least one)
> 0x87366ef81db496edd0ea2055ca605e8686eec1e6 // free ethers !
> 0x08a58f09194e403d02a1928a7bf78646cfc260b0

Specify your chain/network ID if you want an explicit one (default = random)
> 242 // for example. Do not use anything from 1 to 10

Anything fun to embed into the genesis block? (max 32 bytes)
>

What would you like to do? (default = stats)
 1. Show network stats
 2. Manage existing genesis
 3. Track new remote server
 4. Deploy network components
> 2

1. Modify existing fork rules
 2. Export genesis configuration
> 2

Which file to save the genesis into? (default = devnet.json)
> genesis.json
INFO [01-23|15:16:17] Exported existing genesis block

What would you like to do? (default = stats)
 1. Show network stats
 2. Manage existing genesis
 3. Track new remote server
 4. Deploy network components
> ^C // ctrl+C to quit puppeth
```

## 3 Create Chain

Copy genesis File on each Server you would like to create a Ethereum Node

### Initialise the Chain
```
devsrv1$  geth --datadir node1/ init genesis.json
devsrv2$  geth --datadir node2/ init genesis.json
```

### Start the First Node:
```
geth   --identity "coffee_node1"  --networkid 242 --datadir /home/pi/node1 --unlock 0 --password "/home/pi/node1/password.txt" --mine console
```
Congratulations, you just started your own Ethereum node. :)
Now lets get the enode address to connect both nodes together:
```
> admin.nodeInfo.enode
"enode://5e78262b450207237db480afa44616fcd00c1e84fdb25c22847a8f22e83fae702f5281af80fc8bd8447d26689e2bd88d53d6a50ca0ac10433ce482765fed80c5@[::]:30303"
```

Change the IP to your nodes local IP:
```
"enode://5e78262b450207237db480afa44616fcd00c1e84fdb25c22847a8f22e83fae702f5281af80fc8bd8447d26689e2bd88d53d6a50ca0ac10433ce482765fed80c5@192.168.1.10:30303"
```
and save it in a file named `static-nodes.json` on `node2` in the folder `node2`

Now you can start the second node with:
```
geth   --identity "coffee_node2"  --networkid 242 --datadir /home/pi/node2 --unlock 0 --password "/home/pi/node2/password.txt" --mine console
```

Check if both connected:
```
> admin.peers
```
shoud look like this:
```
[{
    caps: ["eth/62", "eth/63"],
    id: "4af5bdfeab8751e610953b49b23a8ad4d3e7ddd3fa9f201f05a348eeaecf746d20427414e5beb0c166ca12b3aaf4a96cd38b29b2e34c4cccde43fb3feabc2292",
    name: "Geth/coffee_core/v1.8.7-stable-66432f38/linux-amd64/go1.10",
    network: {
      inbound: false,
      localAddress: "192.168.1.8:37084",
      remoteAddress: "192.168.1.165:30303",
      static: true,
      trusted: false
    },
    protocols: {
      eth: {
        difficulty: 89805,
        head: "0xffdd48d1927e932158381f6377e76d8208da2c2cb53d911f282033f1160a52c6",
        version: 63
      }
    }
}]
```

## 4 Install Mist Wallet 

https://github.com/ethereum/mist/releases

Add `static-nodes.json` in `.ethereum` directory and start `mist`. You should see `Privat-Net` on the start up screen.

Now You can creat the contract `contract.sol`.


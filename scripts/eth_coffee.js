// Interaction with GPIO
var Gpio = require('onoff').Gpio

//var greenLed = new Gpio(5, 'high')


// Interaction with Ethereum
var Web3 = require('web3')
var web3 = new Web3()

// connect to the local node
web3.setProvider(new web3.providers.HttpProvider('http://localhost:8042'))

// The contract that we are going to interact with
var contractAddress = '0x3F45D4615A21cB534E1b0173EDBCb0305E41da96'

// Define the ABI (Application Binary Interface)
var ABI = JSON.parse('[ { "constant": true, "inputs": [ { "name": "wallet", "type": "address" } ], "name": "getCustomerData", "outputs": [ { "name": "name", "type": "string" }, { "name": "department", "type": "string" }, { "name": "telephone", "type": "string" }, { "name": "email", "type": "string" } ], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [ { "name": "wallet", "type": "address" } ], "name": "addAuthorizedExchangeWallet", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [ { "name": "coffeeMaker", "type": "address" }, { "name": "name", "type": "string" }, { "name": "price", "type": "uint256" } ], "name": "addCoffeeMakerProgram", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [], "name": "kill", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [ { "name": "wallet", "type": "address" }, { "name": "name", "type": "string" }, { "name": "department", "type": "string" }, { "name": "telephone", "type": "string" }, { "name": "email", "type": "string" } ], "name": "addCustomer", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [ { "name": "wallet", "type": "address" } ], "name": "getTokens", "outputs": [ { "name": "tokenss", "type": "uint256", "value": "0" } ], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [ { "name": "wallet", "type": "address" }, { "name": "name", "type": "string" }, { "name": "locDescriptive", "type": "string" }, { "name": "locDepartment", "type": "string" }, { "name": "locLatitude", "type": "string" }, { "name": "locLongitude", "type": "string" }, { "name": "infoMachineType", "type": "uint8" }, { "name": "infoDescription", "type": "string" } ], "name": "addCoffeemaker", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [ { "name": "coffeeMaker", "type": "address" }, { "name": "program", "type": "uint8" }, { "name": "amount", "type": "uint8" } ], "name": "buyCoffee", "outputs": [ { "name": "transferedTokens", "type": "uint256" } ], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [ { "name": "wallet", "type": "address" }, { "name": "program", "type": "uint8" } ], "name": "getCoffeeMakerProgramDetails", "outputs": [ { "name": "name", "type": "string", "value": "" }, { "name": "price", "type": "uint256", "value": "0" } ], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [ { "name": "wallet", "type": "address" } ], "name": "getCoffeeMakerProgramCount", "outputs": [ { "name": "programCount", "type": "uint8", "value": "0" } ], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [ { "name": "wallet", "type": "address" } ], "name": "getCoffeeMakerData", "outputs": [ { "name": "owner", "type": "address" }, { "name": "name", "type": "string" }, { "name": "descriptiveLocation", "type": "string" }, { "name": "department", "type": "string" }, { "name": "latitude", "type": "string" }, { "name": "longitude", "type": "string" }, { "name": "machineType", "type": "uint8" }, { "name": "machineInfo", "type": "string" } ], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [ { "name": "receiver", "type": "address" }, { "name": "tokens", "type": "uint256" } ], "name": "transferTokens", "outputs": [ { "name": "transferedTokens", "type": "uint256" } ], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [ { "name": "seller", "type": "address" }, { "name": "tokens", "type": "uint256" } ], "name": "sellTokens", "outputs": [ { "name": "soldTokens", "type": "uint256" } ], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [ { "name": "buyer", "type": "address" } ], "name": "buyTokens", "outputs": [ { "name": "receivedTokens", "type": "uint256" } ], "payable": true, "stateMutability": "payable", "type": "function" }, { "inputs": [], "payable": false, "stateMutability": "nonpayable", "type": "constructor" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "wallet", "type": "address" } ], "name": "ExchangeWalletAuthorized", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "wallet", "type": "address" } ], "name": "CustomerAdded", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "wallet", "type": "address" }, { "indexed": false, "name": "owner", "type": "address" } ], "name": "CoffeeMakerAdded", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "coffeeMaker", "type": "address" }, { "indexed": false, "name": "name", "type": "string" }, { "indexed": false, "name": "price", "type": "uint256" } ], "name": "CoffeeMakerProgramAdded", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "coffeeMaker", "type": "address" }, { "indexed": false, "name": "program", "type": "uint8" }, { "indexed": false, "name": "amount", "type": "uint8" } ], "name": "CoffeeBought", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "customer", "type": "address" }, { "indexed": false, "name": "tokens", "type": "uint256" } ], "name": "TokensBought", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "customer", "type": "address" }, { "indexed": false, "name": "tokens", "type": "uint256" } ], "name": "TokensSold", "type": "event" }, { "anonymous": false, "inputs": [ { "indexed": false, "name": "sender", "type": "address" }, { "indexed": false, "name": "recipient", "type": "address" }, { "indexed": false, "name": "tokens", "type": "uint256" } ], "name": "TokensTransfered", "type": "event" } ]')

// contract object
var contract = web3.eth.contract(ABI).at(contractAddress)

// components connected to the RPi
var one_coffee = new Gpio(5, 'high')
var two_coffee = new Gpio(6, 'high')

// wait for an event triggered on the Smart Contract
var onValueChanged = contract.CoffeeBought({coffeeMaker: web3.eth.coinbase});

//var amount = contract.CoffeeBought({amount});

//var stringam = "coffee produced: 2";
//var amount = stringam.replace('coffee produced: ','') 
//console.log(amount)



onValueChanged.watch(function(error, result) {
 console.log(result);
 console.log(error);
    if (!error) {
       var amount = +result.args.amount;
        brewCoffee(amount);
 }
})


// power the CoffeeMachine
function brewCoffee(amount) {
    
    console.log('coffee produced:', amount)
    if (amount == 1) {
        // Green: you have enough token
        one_coffee.writeSync(0)
        setTimeout(function () { one_coffee.writeSync(1)}, 1000)

    } 
    if (amount == 2){
        // Red: not enough token
        two_coffee.writeSync(0)
       setTimeout(function () { two_coffee.writeSync(1)}, 1000)
    }
}


// release process
process.on('SIGINT', function () {
    one_coffee.unexport()
    two_coffee.unexport()
})

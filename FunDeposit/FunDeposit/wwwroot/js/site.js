$(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/depositHub")
        .build();

    connection.on("ReceiveDeposits", (user, message) => {
        console.log("received");
        debugger;
    });
    debugger;
    connection.start().catch(function (err) {
        return console.error(err.toString());
    });


    //setTimeout(function () {
    //    connection.invoke("SendDeposits", "asd", "asdfsa").catch(err => console.error(err.toString()));
    //}, 3000);

    DepositUtils = new function () {
        var initViewModel = function (viewModel) {
            var self = this;

            // Mapping VM to ko viewModel
            self = ko.mapping.fromJS(viewModel);

            // Add VM for UI Display
            ko.utils.arrayForEach(self.Deposits(), function (deposit) {
                deposit.PrincipalFormat = ko.computed(function () {
                    return '$' + this.Principal().toLocaleString(undefined, { minimumFractionDigits: 2 });
                }, deposit);

                deposit.StartDateFormat = ko.computed(function () {
                    var dateTime = new moment(this.StartDate());
                    return dateTime.format('DD-MMM-YYYY');
                }, deposit);

                deposit.EndDateFormat = ko.computed(function () {
                    var dateTime = new moment(this.EndDate());
                    return dateTime.format('DD-MMM-YYYY');
                }, deposit);

                deposit.InterestRateFormat = ko.computed(function () {
                    return (this.InterestRate() * 100).toFixed(2);
                }, deposit);

                deposit.MaturityAmountFormat = ko.computed(function () {
                    return '$' + this.MaturityAmount().toLocaleString(undefined, { minimumFractionDigits: 2 });
                }, deposit);
            });

            self.TotalMaturityAmountFormat = ko.computed(function () {
                return '$' + this.TotalMaturityAmount().toLocaleString(undefined, { minimumFractionDigits: 2 });
            }, self);

            return self;
        }

        return {
            init: initViewModel
        }
    }
});
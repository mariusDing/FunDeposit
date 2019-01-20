$(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/depositHub")
        .build();

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    DepositUtils = new function () {
        var initViewModel = function (viewModel) {
            var self = this;

            self = ko.mapping.fromJS(viewModel);

            SetUpPropsForDisplay(self.Deposits);

            self.TotalMaturityAmountFormat = ko.computed(function () {
                return '$' + this.TotalMaturityAmount().toLocaleString(undefined, { minimumFractionDigits: 2 });
            }, self);

            self.Hold = function () {
                clearAllTimeOut();
            }

            self.Buy = function () {
                clearAllTimeOut();

                connection.invoke("BuyDeposit").catch(err => console.error(err.toString()));

                setInterval(function () { connection.invoke("BuyDeposit").catch(err => console.error(err.toString())); }, 5000);
            };

            self.Sell = function () {
                clearAllTimeOut();

                connection.invoke("RemoveDeposit").catch(err => console.error(err.toString()));

                setInterval(function () { connection.invoke("RemoveDeposit").catch(err => console.error(err.toString())); }, 5000);
            };

            function SetUpPropsForDisplay(deposits) {
                ko.utils.arrayForEach(deposits(), function (deposit) {
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
            };

            var BuyCallBack = function (isSuccess, updatedVM) {
                if (isSuccess) {
                    var updatedVM = ko.mapping.fromJS(updatedVM);
                    SetUpPropsForDisplay(updatedVM.Deposits);

                    ko.mapping.fromJS(updatedVM, self);
                } else {
                    alert("Total Maturity Amount Has Reached The Max Amount.");
                }
            }

            var RemoveCallBack = function (isSuccess, updatedVM) {
                if (isSuccess) {
                    var updatedVM = ko.mapping.fromJS(updatedVM);
                    SetUpPropsForDisplay(updatedVM.Deposits);

                    ko.mapping.fromJS(updatedVM, self);
                } else {
                    alert("Total Maturity Amount Has Reached The Min Amount.");
                }
            }

            connection.on("BuyDepositCallback", BuyCallBack);

            connection.on("RemoveDepositCallback", RemoveCallBack);

            function clearAllTimeOut() {
                var highestTimeoutId = setInterval(";");
                for (var i = 0; i < highestTimeoutId; i++) {
                    clearInterval(i);
                }
            }

            return self;
        }

        return {
            init: initViewModel
        }
    }
});
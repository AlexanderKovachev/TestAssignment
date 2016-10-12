var AssignmentApp = angular.module('AssignmentApp', ['ngMessages'])

AssignmentApp.controller('EmployeeController', function ($scope, EmployeeService) {
    var self = this;

    self.message = "This page uses AngularJS to display Employee data";

    self.editEmp = {
        Id: 0,
        Name: '',
        Age: 0,
        TaxPercent: 0
    }

    //functions
    self.getEmployees = function () {
        EmployeeService.getEmployees()
            .success(function (emps) {
                console.log(emps);
                self.employees = emps;
            })

            .error(function (error) {
                self.status = 'Unable to load employee data: ' + error.Message;
                console.log(self.status);
            });
    }

    self.createEmployee = function () {
        self.clear();
        self.toggleSaveForm(true, "Create new employee");
    }

    self.saveEmployee = function () {
        EmployeeService.saveEmployee(self.editEmp)
            .success(function (data) {
                console.log(data);
                self.clear();
                self.toggleSaveForm(false);
                self.getEmployees();
            })

            .error(function (error) {
                self.status = 'Unable to save employee data: ' + error.Message;
                console.log(self.status);
            });
    }

    self.editEmployee = function (emp) {
        self.editEmp.Id = emp.Id;
        self.editEmp.Name = emp.Name;
        self.editEmp.Age = emp.Age;
        self.editEmp.TaxPercent = emp.TaxPercent;

        self.toggleSaveForm(true, "Edit employee");
    }

    self.deleteEmployee = function (emp) {
        self.toggleSaveForm(false);

        if (confirm("Are you sure you want to delete the employee?")) {
            EmployeeService.deleteEmployee(emp.Id)
                .success(function (data) {
                    self.getEmployees();
                    console.log(data);
                })

                .error(function (error) {
                    self.status = 'Unable to delete employee: ' + error.Message;
                    console.log(self.status);
                });
        }
    }

    self.clear = function () {
        self.editEmp.Id = 0;
        self.editEmp.Name = '';
        self.editEmp.Age = 0;
        self.editEmp.TaxPercent = 0;
    }

    self.cancel = function () {
        self.clear();
        self.toggleSaveForm(false);
    }

    self.toggleSaveForm = function (visible, title) {
        if (visible === true)
        {
            self.saveFormTitle = title;
            self.saveFormDisplay = "block";
        }
        else
        {
            self.saveFormDisplay = "none";
        }
    }

    self.toggleSaveForm(false);
    self.getEmployees();
});

AssignmentApp.factory('EmployeeService', ['$http', function ($http) {
    var EmployeeService = {};

    EmployeeService.getEmployees = function () {
        return $http.get('/api/Employee');
    };

    EmployeeService.saveEmployee = function (emp) {
        if (emp.Id == 0)
        {
            return $http.post('/api/Employee', emp);
        }

        return $http.put('/api/Employee/' + emp.Id, emp);
    };

    EmployeeService.deleteEmployee = function (id) {
        return $http.delete('/api/Employee/' + id);
    };

    return EmployeeService;
}]);

AssignmentApp.directive('ageDirective', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, mCtrl) {
            function myValidation(value) {
                if (isNaN(value)) {
                    mCtrl.$setValidity('age', false);
                } else if (value < 18 || value > 67) {
                    mCtrl.$setValidity('age', false);
                } else {
                    mCtrl.$setValidity('age', true);
                }
                return value;
            }
            mCtrl.$parsers.push(myValidation);
        }
    };
});
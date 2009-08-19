using System;
using System.ComponentModel;
using System.Text;
using SpecExpress;
using SpecExpress.Test.Domain.Entities;

namespace SampleSilverlightApp
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private Project _currentProject;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageModel(Project currentProject)
        {
            _currentProject = currentProject;
        }

        /// <summary>
        /// A proxy to the ProjectName on the entity.
        /// Setter is required to do any validation on the proposed value and if
        /// invalid then an exception is thrown containing a message to disply to the user.
        /// 
        /// Sucky restrictions needed to overcome:
        /// - Silverlight assumes that the property's value did not change if invalid.
        /// 
        ///     Impact: SpecExpress validates the entity as a whole.  Since rules associated with other
        ///     properties may reference this property (i.e. Custom Rule), we must set the property 
        ///     on the instance.  
        /// 
        ///     Example: Consider an entity having a StartDate and EndDate property with rule for StartDate
        ///     dictating that StartDate must be before EndDate, and a rule for EndDate dictating that 
        ///     EndDate must be after StartDate.  If I change the EndDate to a value that is before StartDate,
        ///     both rules are broken.  In order to know that both rules are broken, we can't just evaluate the 
        ///     rules for EndDate when it changes - we must evaluate all rules for all properties on the entity.
        ///     
        ///     Furthermore, since the rule for StartDate compairs itself to the value in the EndDate property,
        ///     the EndDate property must be set to the proposed value - we can't just fool the system into looking
        ///     at some "proposed" value for EndDate since the rule for StartDate relies on that same "proposed"
        ///     value for validation.
        /// 
        /// - The message thrown in the exception is tied to the control bound to the property.
        ///     Impact: Since a given property value may trigger other properties to be invalid, we need to somehow
        ///     update the view in such a way that any field bound to a property that is associated with a broken
        ///     rule is updated.  The below solution really is not suitable since it consolodates all the messages
        ///     into the single bound control.
        /// 
        ///     Example: Consider the same entity having StartDate and EndDate as described above.  When either
        ///     StartDate or EndDate are invalid, both are invalid, causing two rules to break and two 
        ///     ValidationResults - one tied to StartDate and one tied to EndDate.  The user interface needs
        ///     to reflect that both dates are invalid.  
        /// </summary>
        public string ProjectName
        {
            get { return _currentProject.ProjectName; }
            set
            {
                // Store off old propertyValue
                //   (hack - storing the original value and calling the entity's setter with the original
                //    value is not a best practice since it does not ensure original state of the entity and
                //    may trigger invalid events that are raised by the property setter (if entity's property
                //    raises events))
                string oldValue = _currentProject.ProjectName;

                // Set the new property on the domain entity so it can be validated.
                _currentProject.ProjectName = value;

                // Validate entity
                var validationResults = ValidationContainer.Validate(_currentProject);

                if (!validationResults.IsValid)
                {
                    // Houston we have a problem - build a message to be displayed
                    //  (hack - not feasable to put all messages together since some messages may not be associated
                    //   with the calling control)
                    StringBuilder sb = new StringBuilder();
                    foreach (var er in validationResults.Errors)
                    {
                        sb.AppendLine(er.Message);
                    }
                    // Set Entity back to initial state
                    _currentProject.ProjectName = oldValue;
                    // Throw exception with message to be displayed so binding can handle it.
                    throw new ArgumentException(sb.ToString());
                }
                else
                {
                    // Everything looks great - Notify view that property changed
                    OnPropertyChanged("ProjectName");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
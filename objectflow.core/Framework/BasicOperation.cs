﻿using System.Collections.Generic;
using Rainbow.ObjectFlow.Interfaces;

namespace Rainbow.ObjectFlow.Framework
{
    /// <summary>
    /// Implements common functionality for Operation objects.  
    /// All operations must inherit from BasicOperation.
    /// </summary>
    /// <typeparam name="T">Type of object this operation will work on.
    /// </typeparam>
    /// <example>
    /// 
    /// </example>
    public abstract class BasicOperation<T> : IOperation<T>
    {
        private bool _resultSetByInheritedObject;

        /// <summary>
        /// Executes the operation
        /// </summary>
        /// <param name="data">Data to perform transformations on</param>
        /// <returns>The operation results as an IEnumerable of T</returns>
        public abstract IEnumerable<T> Execute(IEnumerable<T> data);

        /// <summary>
        /// Default constructor
        /// </summary>
        protected BasicOperation()
        {
            SuccessResult = false;
        }

        /// <summary>
        /// returns true if the operation succeeded, false otherwise.  
        /// </summary>
        /// <remarks>
        /// The default behaviour is to set this to True when the operation completes without Exception
        /// Use the SetSuccessResult virtual method to set this property.
        /// </remarks>
        public bool SuccessResult { get; private set; }

        /// <summary>
        /// This method is called to set operation success.
        /// Override this method to define custom success criteria.
        /// </summary>
        /// <param name="succeeded">The value to set the SuccessResult property to.</param>
        /// <returns>Returns SuccessResult property after this method has set it.  This should be the same as the value passed to it.</returns>
        /// <example>
        /// Override example
        /// </example>
        public virtual bool SetSuccessResult(bool succeeded)
        {
            if (!_resultSetByInheritedObject)
            {
                _resultSetByInheritedObject = true;
                SuccessResult = succeeded;
            }
            return SuccessResult;
        }
    }
}
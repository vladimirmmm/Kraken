var TSLinq;
(function (TSLinq) {
    var Linq = (function () {
        function Linq(a) {
            if (a === void 0) { a = []; }
            this.a = a;
            Object.defineProperty(this, "a", { value: a, writable: false });
        }
        Linq.prototype.Aggregate = function (func, initialValue) {
            var a;
            var current;
            if ((a = this.a).length === 0)
                throw "Aggregate of empty array";
            current = a[0];
            for (var i = 1, n = a.length; i < n; ++i) {
                current = func(current, a[i]);
            }
            return current;
        };
        Linq.prototype.All = function (predicate) {
            var a = this.a;
            for (var i = 0, n = a.length; i < n; ++i) {
                if (!predicate(a[i])) {
                    return false;
                }
            }
            return true;
        };
        Linq.prototype.Any = function (predicate) {
            var a = this.a;
            for (var i = 0, n = a.length; i < n; ++i) {
                if (predicate(a[i])) {
                    return true;
                }
            }
            return false;
        };
        Linq.prototype.Average = function (selector) {
            var a = this.a;
            selector = selector || (function (o) { return o; });
            var total = 0;
            var len = a.length;
            for (var i = 0; i < len; ++i) {
                total += selector(a[i]);
            }
            return total / len;
        };
        Linq.prototype.Concat = function (array) {
            return new Linq(this.a.concat(array));
        };
        Linq.prototype.Contains = function (value, comparer) {
            if (!comparer)
                return this.Any(function (o) { return o === value; });
            return this.Any(function (o) { return comparer.Equals(o, value); });
        };
        Linq.prototype.Count = function (selector) {
            if (selector)
                return this.Where(selector).Count();
            return this.a.length;
        };
        Linq.prototype.Distinct = function (comparer) {
            return this.DistinctBy(function (o) { return o; }, comparer);
        };
        Linq.prototype.DistinctBy = function (selector, comparer) {
            var a = this.a;
            var e;
            var keys = [], unique = [];
            for (var i = 0, n = a.length; i < n; ++i) {
                e = a[i];
                var objKey = selector(e);
                if (!keys.AsLinq().Contains(objKey, comparer)) {
                    keys.push(objKey);
                    unique.push(e);
                }
            }
            return new Linq(unique);
        };
        Linq.prototype.ElementAt = function (index) {
            if (index < 0 || index >= this.a.length)
                throw "Index was out of range. Must be non-negative and less than the size of the collection.";
            return this.a[index];
        };
        Linq.prototype.ElementAtOrDefault = function (index, defaultValue) {
            if (index >= this.a.length || index < 0)
                return defaultValue;
            return this.a[index];
        };
        Linq.prototype.Except = function (except, comparer) {
            var a = this.a;
            var result = [];
            var hashTable = {};
            var e, eHash;
            var getHash = comparer ? comparer.GetHashCode : function (e) { return Object.GetHashCode(e); };
            for (var i = 0, n = except.length; i < n; ++i) {
                hashTable[getHash(except[i])] = 1;
            }
            for (var i = 0, n = a.length; i < n; ++i) {
                e = a[i];
                eHash = getHash(e);
                if (!hashTable[eHash])
                    result.push(e);
            }
            return new Linq(result);
        };
        Linq.prototype.First = function (selector) {
            if (this.a.length === 0)
                throw "Enumeration does not contain elements";
            if (!selector)
                return this.a[0];
            var result = this.Where(selector);
            if (result.Count() === 0)
                throw "Enumeration does not contain elements";
            return result.ElementAt(0);
        };
        Linq.prototype.FirstOrDefault = function (selector, defaultValue) {
            if (!selector)
                return this.a.length > 0 ? this.a[0] : defaultValue;
            return this.Where(selector).ElementAtOrDefault(0, defaultValue);
        };
        Linq.prototype.ForEach = function (callback) {
            var a = this.a;
            for (var i = 0, n = a.length; i < n; ++i) {
                if (callback(a[i], i) === false)
                    break;
            }
        };
        Linq.prototype.GroupBy = function (keySelector, elementSelector, comparer) {
            elementSelector = elementSelector || (function (o) { return o; });
            comparer = comparer || { Equals: function (a, b) { return a == b; }, GetHashCode: function (e) { return Object.GetHashCode(e); } };
            var a = this.a;
            var key, hashKey, reHashKey;
            var hashs = {};
            for (var i = 0, n = a.length; i < n; ++i) {
                reHashKey = undefined;
                key = keySelector(a[i]);
                hashKey = comparer.GetHashCode(key);
                if (typeof hashs[hashKey] !== "undefined")
                    reHashKey = comparer.Equals(key, hashs[hashKey].Key) ? hashKey : hashKey + i;
                if (typeof reHashKey !== "undefined" && reHashKey !== hashKey)
                    hashKey = reHashKey;
                hashs[hashKey] = hashs[hashKey] || { Key: key, Elements: [] };
                hashs[hashKey].Elements.push(elementSelector(a[i]));
            }
            var keys = Object.keys(hashs);
            var ret = [];
            for (var i = 0, n = keys.length; i < n; ++i) {
                ret.push(hashs[keys[i]]);
            }
            return new Linq(ret);
        };
        Linq.prototype.IndexOf = function (e, comparer) {
            var a = this.a;
            if (comparer) {
                for (var i = 0, n = a.length; i < n; ++i) {
                    var ce = a[i];
                    if (comparer.Equals(ce, e)) {
                        return i;
                    }
                }
            }
            else {
                for (var i = 0, n = a.length; i < n; ++i) {
                    if (a[i] === e) {
                        return i;
                    }
                }
            }
            return -1;
        };
        Linq.prototype.Intersect = function (array, comparer) {
            var result = [];
            for (var i = 0, n = array.length; i < n; ++i) {
                if (this.Contains(array[i], comparer)) {
                    result.push(array[i]);
                }
            }
            return new Linq(result);
        };
        Linq.prototype.Join = function (array, outerKeySelector, innerKeySelector, resultSelector, comparer) {
            var result = [];
            var outer = this.Select(outerKeySelector);
            var inner = array.AsLinq().Select(innerKeySelector);
            for (var i = 0, n = outer.Count(); i < n; ++i) {
                var outerKey = outer.ElementAt(i);
                var index = -1;
                if ((index = inner.IndexOf(outerKey, comparer)) != -1) {
                    var innerKey = inner.ElementAt(index);
                    result.push(resultSelector(outerKey, innerKey));
                }
            }
            return new Linq(result);
        };
        Linq.prototype.Last = function (predicate) {
            if (this.a.length === 0)
                throw "Enumeration does not contain elements";
            if (!predicate)
                return this.a[this.a.length - 1];
            var result = this.Where(predicate);
            if (result.Count() === 0)
                throw "Enumeration does not contain elements";
            return result.Last();
        };
        Linq.prototype.LastOrDefault = function (predicate, defaultValue) {
            if (this.a.length === 0)
                return defaultValue;
            if (!predicate)
                return this.a[this.a.length - 1];
            var result = this.Where(predicate);
            if (result.Count() === 0)
                return defaultValue;
            return result.LastOrDefault(null, defaultValue);
        };
        Linq.prototype.Max = function (selector) {
            var a = this.a;
            if (a.length === 0)
                throw "Sequence contains no elements.";
            selector = selector || (function (o) { return o; });
            var max = selector(a[0]);
            for (var i = 0, n = a.length; i < n; ++i) {
                var next = selector(a[i]);
                if (next > max)
                    max = next;
            }
            return max;
        };
        Linq.prototype.Min = function (selector) {
            var a = this.a;
            if (a.length === 0)
                throw "Sequence contains no elements.";
            selector = selector || (function (o) { return o; });
            var min = selector(a[0]);
            for (var i = 0, n = a.length; i < n; ++i) {
                var next = selector(a[i]);
                if (next < min)
                    min = next;
            }
            return min;
        };
        Linq.prototype.OrderBy = function (keySelector, comparer) {
            comparer = comparer || (function (a, b) { return a > b ? 1 : -1; });
            this.a.sort(function (l, r) { return comparer(keySelector(l), keySelector(r)); });
            return this;
        };
        Linq.prototype.OrderByDescending = function (keySelector, comparer) {
            comparer = comparer || (function (a, b) { return a > b ? 1 : -1; });
            comparer = (function (comparer) {
                return function (a, b) { return -comparer(a, b); };
            })(comparer);
            this.a.sort(function (l, r) { return comparer(keySelector(l), keySelector(r)); });
            return this;
        };
        Linq.prototype.Reverse = function () {
            var right = this.a.length - 1;
            for (var left = 0; left < right; ++left, --right) {
                var temporary = this.a[left];
                this.a[left] = this.a[right];
                this.a[right] = temporary;
            }
            return this;
        };
        Linq.prototype.Select = function (selector) {
            var a = this.a;
            var result = [];
            for (var i = 0, n = a.length; i < n; ++i) {
                result.push(selector(a[i]));
            }
            return new Linq(result);
        };
        Linq.prototype.SelectMany = function (selector, resultSelector) {
            var a = this.a;
            var result = [];
            for (var i = 0, n = a.length; i < n; ++i) {
                result = result.concat(selector(a[i]));
            }
            if (!resultSelector)
                return result.AsLinq();
            else
                return result.AsLinq().Select(resultSelector);
        };
        Linq.prototype.SequenceEqual = function (second, comparer) {
            var a = this.a;
            if (typeof a === "undefined" || typeof second === "undefined") {
                throw "Do not pass null values to arrays.";
            }
            if (a === second) {
                return true;
            }
            if (a.length !== second.length) {
                return false;
            }
            if (comparer) {
                for (var i = 0, n = a.length; i < n; i++) {
                    if (!comparer(a[i], second[i]))
                        return false;
                }
            }
            else {
                for (var i = 0, n = a.length; i < n; i++) {
                    if (a[i] !== second[i])
                        return false;
                }
            }
            return true;
        };
        Linq.prototype.Single = function (predicate) {
            var a = this.a;
            if (!predicate) {
                if (a.length != 1)
                    throw "Source has none or more than one element";
                return a[0];
            }
            var found = null;
            for (var i = 0, n = a.length; i < n; ++i) {
                if (predicate(a[i])) {
                    if (found != null)
                        throw "Source has more than one element";
                    found = a[i];
                }
            }
            return found;
        };
        Linq.prototype.SingleOrDefault = function (predicate, defaultValue) {
            var a = this.a;
            if (!predicate) {
                if (a.length != 1)
                    return defaultValue;
                return a[0];
            }
            var found = null;
            for (var i = 0, n = a.length; i < n; ++i) {
                if (predicate(a[i])) {
                    if (found != null)
                        return defaultValue;
                    found = a[i];
                }
            }
            return found;
        };
        Linq.prototype.Skip = function (count) {
            return new Linq(this.a.slice(count));
        };
        Linq.prototype.SkipWhile = function (predicate) {
            var a = this.a, i = 0;
            for (var n = a.length; i < n; ++i) {
                if (predicate(a[i]) === false)
                    break;
            }
            return new Linq(a.slice(i));
        };
        Linq.prototype.Sum = function (selector) {
            var a = this.a;
            var result = 0;
            if (selector) {
                for (var i = 0, n = a.length; i < n; ++i) {
                    result += selector(a[i]);
                }
            }
            else {
                for (var i = 0, n = a.length; i < n; ++i) {
                    result += a[i];
                }
            }
            return result;
        };
        Linq.prototype.Take = function (count) {
            var a = this.a;
            var result = [];
            var len = count > (len = a.length) ? len : count;
            for (var i = 0; i < len; ++i) {
                result.push(a[i]);
            }
            return new Linq(result);
        };
        Linq.prototype.TakeWhile = function (predicate) {
            var a = this.a;
            var result = [];
            for (var i = 0, n = a.length, e; i < n; ++i) {
                e = a[i];
                if (predicate(e)) {
                    result.push(e);
                }
                else {
                    break;
                }
            }
            return new Linq(result);
        };
        Linq.prototype.Union = function (second, comparer) {
            var a = this.a;
            var result = [];
            var hashTable = {};
            var e, eHash;
            var getHash = comparer ? comparer.GetHashCode : function (e) { return Object.GetHashCode(e); };
            for (var i = 0, n = a.length; i < n; ++i) {
                e = a[i];
                eHash = getHash(e);
                if (!hashTable[eHash]) {
                    hashTable[eHash] = e;
                    result.push(e);
                }
            }
            for (var i = 0, n = second.length; i < n; ++i) {
                e = second[i];
                eHash = getHash(e);
                if (!hashTable[eHash]) {
                    hashTable[eHash] = e;
                    result.push(e);
                }
            }
            return new Linq(result);
        };
        Linq.prototype.Where = function (selector) {
            var a = this.a;
            var e;
            var result = [];
            for (var i = 0, n = a.length; i < n; ++i) {
                e = a[i];
                if (selector(e)) {
                    result.push(e);
                }
            }
            return new Linq(result);
        };
        Linq.prototype.Zip = function (array, resultSelector) {
            var a = this.a;
            var result = [];
            var len = a.length > array.length ? array.length : a.length;
            for (var i = 0, n = len; i < n; ++i) {
                result.push(resultSelector(a[i], array[i]));
            }
            return new Linq(result);
        };
        Linq.prototype.ToArray = function () {
            return this.a;
        };
        return Linq;
    })();
    TSLinq.Linq = Linq;
})(TSLinq || (TSLinq = {}));
Array.prototype.AsLinq = function () {
    return new TSLinq.Linq(this);
};
Object.GetHashCode = function (e) {
    if (e instanceof Number)
        return Number.GetHashCode(e);
    var s = e instanceof Object ? JSON.StringifyNonCircular(e) : e.toString();
    var hash = 0;
    if (s.length === 0)
        return hash;
    for (var i = 0; i < s.length; ++i) {
        hash = ((hash << 5) - hash) + s.charCodeAt(i);
    }
    return hash;
};
Number.GetHashCode = function (e) {
    return e.valueOf();
};
Object.IsPlain = function (e) {
    if ((typeof (e) !== "object" || e.nodeType || (e instanceof Window)) || (e.constructor && !({}).hasOwnProperty.call(e.constructor.prototype, "isPrototypeOf"))) {
        return false;
    }
    return true;
};
JSON.StringifyNonCircular = function (obj) {
    var s = s || "";
    for (var i in obj) {
        var o = obj[i];
        if (o && (o instanceof Array || o.IsPlain())) {
            s += i + ":" + JSON.stringify(o);
        }
        else if (o && typeof o === "object") {
            s += i + ":" + "$ref#" + o;
        }
        else {
            s += i + ":" + o;
        }
    }
    return s;
};
//# sourceMappingURL=Linq.js.map